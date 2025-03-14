using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.Message;
using OrbitMap.API.Payload.Response.Message;
using OrbitMap.API.Payload.Response.Story;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Implement;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Persistent;
using OrbitMap.Domain.Utils;
using OrbitMap.Repository.Interfaces;
using Group = OrbitMap.Domain.Entities.Group;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.SignalR;

[Authorize]
public class MessageHub : Hub
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IOneSignalService _oneSignalService;
    private readonly IHubContext<PresenceHub> _presenceHub;
    private readonly PresenceTracker _tracker;
    private readonly IUnitOfWork<OrbitMapContext> _unitOfWork;

    public MessageHub(IUnitOfWork<OrbitMapContext> unitOfWork,
        IMapper mapper, PresenceTracker tracker,
        IHubContext<PresenceHub> presenceHub,
        ILogger logger, IConfiguration config, IOneSignalService oneSignalService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tracker = tracker;
        _presenceHub = presenceHub;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var otherUser = httpContext.Request.Query["user"].ToString();
        if (otherUser.Equals("undefined")) throw new HubException("User not found");
        var groupName = GetGroupName(Context.User.Identity.Name, otherUser);
        _logger.Information($"Group name: {groupName}");
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var group = await AddToGroup(groupName);
        var messages = await GetMessageThread(Context.User.Identity.Name, otherUser);
        await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var group = await RemoveFromMessageGroup();
        _logger.Information($"Group Test: {group.Name} ");
        _logger.Information($"Connection Test: {Context.ConnectionId} ");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.Name);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(CreateMessageDto createMessageDto)
    {
        var username = Context.User.GetUsername();
        if (username == createMessageDto.RecipientUsername.ToLower())
            throw new HubException("You cannot send message to yourself");
        var users = await _unitOfWork.GetRepository<Member>().GetListAsync(
            predicate: u => u.Username.Equals(username) || u.Username.Equals(createMessageDto.RecipientUsername)
        );
        var sender = users.SingleOrDefault(x => x.Username.Equals(username));
        var recipient = users.SingleOrDefault(x => x.Username.Equals(createMessageDto.RecipientUsername));
        if (recipient == null) throw new HubException("Not found recipient user");

        Story? story = null;
        if (createMessageDto.StoryId.HasValue)
        {
            story = await _unitOfWork.GetRepository<Story>().SingleOrDefaultAsync(
                predicate: x => x.Id == createMessageDto.StoryId && x.IsDisabled == false
            );
            if (story == null) throw new HubException("Story not found");
        }

        var message = new Message
        {
            Id = Guid.NewGuid(),
            MessageDocument = new MessageDocument()
            {
                SenderUsername = sender.Username,
                RecipientUsername = recipient.Username,
                StoryId = story?.Id,
                CreatedDate = TimeUtil.GetCurrentSEATime(),
                Content = createMessageDto.Content,
                IsSticker = createMessageDto.IsSticker
            }
        };
        var groupName = GetGroupName(sender.Username, recipient.Username);
        var group = await _unitOfWork.GetRepository<Group>().SingleOrDefaultAsync(
            predicate: x => x.Name == groupName,
            include: x => x.Include(g => g.Connections)
        );
        if (group.Connections.Any(x => x.UserName == recipient.Username))
            message.MessageDocument.DateRead = TimeUtil.GetCurrentSEATime();

        await _unitOfWork.GetRepository<Message>().InsertAsync(message);
        await UpdateLastMessageChat(message, sender, recipient);
        if (await _unitOfWork.CommitAsync() > 0)
        {
            var messageDto = new MessageDto()
            {
                Id = message.Id,
                SenderUsername = message.MessageDocument.SenderUsername,
                RecipientUsername = message.MessageDocument.RecipientUsername,
                Content = message.MessageDocument.Content,
                MessageSent = message.MessageDocument.CreatedDate,
                DateRead = message.MessageDocument.DateRead,
                StoryId = message.MessageDocument.StoryId ?? Guid.Empty,
                IsSticker = message.MessageDocument.IsSticker
            };
            if (story != null)
            {
                messageDto.Story = _mapper.Map<StoryResponse>(story);
            }

            await Clients.Group(groupName).SendAsync("NewMessage", messageDto);
            var connections = await _tracker.GetConnectionsForUser(createMessageDto.RecipientUsername);
            if (connections != null)
            {
                var member = _mapper.Map<UserDto>(sender);
                await _presenceHub.Clients.Clients(connections)
                    .SendAsync("NewMessageReceived", member, createMessageDto.Content);
                string messageSend = null;
                if (message.MessageDocument.StoryId.HasValue) messageSend = $"😊 {sender.DisplayName} reply your story";
                else
                    messageSend = $"😊 {sender.DisplayName} send a message to you";
                BackgroundJob.Enqueue<NotificationService>(
                    service => service.SendNotificationToUser(sender.DisplayName, createMessageDto.RecipientUsername,
                        messageSend)
                );
            }
        }
    }

    private async Task UpdateLastMessageChat(Message message, Member sender, Member recipient)
    {
        var lastMessageFromDb = await _unitOfWork.GetRepository<LastMessageChat>().SingleOrDefaultAsync(
            predicate: x =>
                (x.LastMessageChatDocument.SenderUsername == message.MessageDocument.SenderUsername &&
                 x.LastMessageChatDocument.RecipientUsername == message.MessageDocument.RecipientUsername) ||
                (x.LastMessageChatDocument.SenderUsername == message.MessageDocument.RecipientUsername &&
                 x.LastMessageChatDocument.RecipientUsername == message.MessageDocument.SenderUsername)
        );
        if (lastMessageFromDb != null)
        {
            lastMessageFromDb.LastMessageChatDocument.Content = message.MessageDocument.Content;
            lastMessageFromDb.LastMessageChatDocument.MessageLastDate = message.MessageDocument.CreatedDate;
            //neu user online thi isRead = true, mac dinh la false
            //if (await _presenceTracker.CheckUsernameIsOnline(message.RecipientUsername!))
            //    lastMessageFromDb.IsRead = true;
            //else
            //    lastMessageFromDb.IsRead = false;
            _unitOfWork.GetRepository<LastMessageChat>().UpdateAsync(lastMessageFromDb);
        }
        else
        {
            var groupName = GetGroupName(message.MessageDocument.SenderUsername,
                message.MessageDocument.RecipientUsername);
            var lastMessageChat = new LastMessageChat
            {
                Id = Guid.NewGuid(),
                LastMessageChatDocument = new LastMessageChatDocument
                {
                    Content = message.MessageDocument.Content,
                    MessageLastDate = message.MessageDocument.CreatedDate,
                    SenderUsername = message.MessageDocument.SenderUsername,
                    RecipientUsername = message.MessageDocument.RecipientUsername,
                    GroupName = groupName,
                    SenderAvatarUrl = sender.AvatarUrl,
                    RecipientAvatarUrl = recipient.AvatarUrl
                }
            };
            //neu user online thi isRead = true, mac dinh la false
            //if (await _presenceTracker.CheckUsernameIsOnline(message.RecipientUsername!))
            //{
            //    lastMessageChat.IsRead = true;
            //}
            await _unitOfWork.GetRepository<LastMessageChat>().InsertAsync(lastMessageChat);
        }
    }

    //Tạo group name dựa trên tên của 2 người chat
    private string GetGroupName(string caller, string other)
    {
        var stringCompare = string.CompareOrdinal(caller, other) < 0;
        return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
    }

    private async Task<Group> AddToGroup(string groupName)
    {
        _logger.Information($"Group Name: {groupName}");
        var group = await _unitOfWork.GetRepository<Group>().SingleOrDefaultAsync(
            predicate: x => x.Name.Equals(groupName),
            include: x => x.Include(g => g.Connections)
        );
        var connection = new Connection(Context.ConnectionId, Context.User.Identity.Name)
        {
            GroupName = groupName
        };
        if (group == null)
        {
            group = new Group(groupName);
            await _unitOfWork.GetRepository<Group>().InsertAsync(group);
        }

        await _unitOfWork.GetRepository<Connection>().InsertAsync(connection);
        var isSuccessful = await _unitOfWork.CommitAsync() > 0;
        if (isSuccessful) return group;

        throw new HubException("Failed to join group");
    }

    private async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
    {
        var messages = await _unitOfWork.GetRepository<Message>().GetListAsync(
            selector: x => new MessageDto
            {
                Id = x.Id,
                SenderUsername = x.MessageDocument.SenderUsername,
                RecipientUsername = x.MessageDocument.RecipientUsername,
                Content = x.MessageDocument.Content,
                MessageSent = x.MessageDocument.CreatedDate,
                DateRead = x.MessageDocument.DateRead,
                StoryId = x.MessageDocument.StoryId ?? Guid.Empty,
                IsSticker = x.MessageDocument.IsSticker
            },
            predicate: x =>
                (x.MessageDocument.RecipientUsername == currentUsername &&
                 x.MessageDocument.SenderUsername == recipientUsername) ||
                (x.MessageDocument.RecipientUsername == recipientUsername &&
                 x.MessageDocument.SenderUsername == currentUsername),
            orderBy:
            x => x.OrderBy(m => m.MessageDocument.CreatedDate)
        );
        var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUsername).ToList();
        if (unreadMessages.Any())
            foreach (var mess in unreadMessages)
                mess.DateRead = TimeUtil.GetCurrentSEATime();
        foreach (var message in messages)
        {
            if (message.StoryId != Guid.Empty)
            {
                var story = await _unitOfWork.GetRepository<Story>().SingleOrDefaultAsync(
                    predicate: x => x.Id.Equals(message.StoryId)
                );
                message.Story = _mapper.Map<StoryResponse>(story);
            }
        }

        return messages;
    }

    private async Task<Group> RemoveFromMessageGroup()
    {
        _logger.Information($"ConnectionId: {Context.ConnectionId}");
        var group = await _unitOfWork.GetRepository<Group>().SingleOrDefaultAsync(
            predicate: x => x.Connections.Any(c => c.ConnectionId == Context.ConnectionId),
            include: x => x.Include(x => x.Connections)
        );
        var connection = await _unitOfWork.GetRepository<Connection>().SingleOrDefaultAsync(
            predicate: x => x.ConnectionId == Context.ConnectionId
        );
        _unitOfWork.GetRepository<Connection>().DeleteAsync(connection);

        var isSuccessful = await _unitOfWork.CommitAsync() > 0;
        if (isSuccessful) return group;

        throw new HubException("Fail to remove from group");
    }
}