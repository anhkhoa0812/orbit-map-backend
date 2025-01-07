using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.Message;
using OrbitMap.API.Payload.Response.Message;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Implement;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.Validators;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using Group = OrbitMap.Domain.Entities.Group;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.SignalR;

[Authorize]
public class MessageHub : Hub
{
    private readonly IUnitOfWork<OrbitMapContext> _unitOfWork;
    private readonly IHubContext<PresenceHub> _presenceHub;
    private readonly IMapper _mapper;
    private readonly PresenceTracker _tracker;
    private readonly ILogger _logger;
    private readonly IConfiguration _config;
    private readonly IOneSignalService _oneSignalService;
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
        _config = config;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var otherUser = httpContext.Request.Query["user"].ToString();
        if(otherUser.Equals("undefined")) throw new HubException("User not found");
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
        if(username == createMessageDto.RecipientUsername.ToLower()) 
            throw new HubException("You cannot send message to yourself");
        var users = await _unitOfWork.GetRepository<User>().GetListAsync(
            predicate: u => u.Username.Equals(username) || u.Username.Equals(createMessageDto.RecipientUsername)
        );
        var sender = users.SingleOrDefault(x => x.Username.Equals(username));
        var recipient = users.SingleOrDefault(x => x.Username.Equals(createMessageDto.RecipientUsername));
        if (recipient == null) throw new HubException("Not found recipient user");
        var message = new Message
        {
            Id = Guid.NewGuid(),
            SenderId = sender.Id,
            RecipientId = recipient.Id,
            SenderUsername = sender.Username,
            RecipientUsername = recipient.Username,
            Content = createMessageDto.Content,
            CreatedDate = DateTime.UtcNow
        };
        var groupName = GetGroupName(sender.Username, recipient.Username);
        var group = await _unitOfWork.GetRepository<Group>().SingleOrDefaultAsync(
            predicate: x => x.Name == groupName,
            include: x => x.Include(g => g.Connections)
        );
        if (group.Connections.Any(x => x.UserName == recipient.Username))
        {
            message.DateRead = DateTime.UtcNow;
        }
        
        await _unitOfWork.GetRepository<Message>().InsertAsync(message);
        await UpdateLastMessageChat(message);
        if (await _unitOfWork.CommitAsync() > 0)
        {
            await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
            var connections = await _tracker.GetConnectionsForUser(createMessageDto.RecipientUsername);
            if (connections != null)
            {
                var user = _mapper.Map<UserDto>(sender);
                await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived", user, createMessageDto.Content);
                string messageSend = $"😊 {sender.DisplayName} send a message to you";
                BackgroundJob.Enqueue<NotificationService>(
                    service => service.SendNotificationToUser(sender.DisplayName, createMessageDto.RecipientUsername, messageSend)
                );
            }
        }
    }

    private async Task UpdateLastMessageChat(Message message)
    {
        var lastMessageFromDb = await _unitOfWork.GetRepository<LastMessageChat>().SingleOrDefaultAsync(
            predicate: x => x.SenderUsername == message.SenderUsername && x.RecipientUsername == message.RecipientUsername ||
                            x.SenderUsername == message.RecipientUsername && x.RecipientUsername == message.SenderUsername
        );
        if (lastMessageFromDb != null)
        {
            lastMessageFromDb.Content = message.Content;
            lastMessageFromDb.MessageLastDate = message.CreatedDate;
            //neu user online thi isRead = true, mac dinh la false
            //if (await _presenceTracker.CheckUsernameIsOnline(message.RecipientUsername!))
            //    lastMessageFromDb.IsRead = true;
            //else
            //    lastMessageFromDb.IsRead = false;
            _unitOfWork.GetRepository<LastMessageChat>().UpdateAsync(lastMessageFromDb);
        }
        else
        {
            var groupName = GetGroupName(message.SenderUsername, message.RecipientUsername);
            var lastMessageChat = new LastMessageChat()
            {
                Content = message.Content,
                MessageLastDate = message.CreatedDate,
                SenderId = message.SenderId,
                RecipientId = message.RecipientId,
                SenderUsername = message.SenderUsername,
                RecipientUsername = message.RecipientUsername,
                GroupName = groupName
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
        bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
        if (isSuccessful) return group;
        
        throw new HubException("Failed to join group");
    }
    
    private async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
    {
        var messages = await _unitOfWork.GetRepository<Message>().GetListAsync(
            selector: x => new MessageDto
            {
                Id = x.Id,
                SenderId = x.Sender.Id,
                SenderUsername = x.Sender.Username,
                SenderPhotoUrl = x.Sender.AvatarUrl,
                SenderDisplayName = x.Sender.DisplayName,
                RecipientId = x.RecipientId,
                RecipientUsername = x.RecipientUsername,
                RecipientDisplayName = x.Recipient.DisplayName,
                RecipientPhotoUrl = x.Recipient.AvatarUrl,
                Content = x.Content,
                MessageSent = x.CreatedDate,
                DateRead = x.DateRead
            },
            predicate: x => x.Recipient.Username == currentUsername && x.Sender.Username == recipientUsername || x.Recipient.Username == recipientUsername && x.Sender.Username == currentUsername,
            orderBy: x => x.OrderBy(m => m.CreatedDate),
            include: x => x.Include(m => m.Sender).Include(m => m.Recipient)
        );
        var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUsername).ToList();
        if (unreadMessages.Any())
        {
            foreach (var mess in unreadMessages)
            {
                mess.DateRead = DateTime.UtcNow;
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
        
        bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
        if (isSuccessful) return group;
        
        throw new HubException("Fail to remove from group");
    }
}