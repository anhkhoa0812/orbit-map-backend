using System.Security.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Request.Message;
using OrbitMap.API.Payload.Request.Story;
using OrbitMap.API.Payload.Response.Message;
using OrbitMap.API.Payload.Response.Story;
using OrbitMap.API.Services.Interface;
using OrbitMap.API.SignalR;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class StoryService : BaseService<StoryService>, IStoryService
{
    private readonly IUploadService _uploadService;
    private readonly IHubContext<MessageHub> _hubContext;

    public StoryService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IUploadService uploadService,
        IHubContext<MessageHub> hubContext) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _uploadService = uploadService;
        _hubContext = hubContext;
    }

    public async Task<StoryResponse> CreateStoryAsync(string username, CreateStoryRequest createStoryRequest)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Unauthorized");

        var user = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (user == null)
            throw new AuthenticationException("Unauthorized");
        var story = _mapper.Map<Story>(createStoryRequest);
        var extension = Path.GetExtension(createStoryRequest.ImageFile.FileName).ToLower();
        var allowedImageExtensions = new[] { ".jgeg", ".png", ".jpg", ".gif", ".bmp", ".webp" };
        if (createStoryRequest.ImageFile != null)
        {
            if (extension.Equals(".mp4"))
            {
                if (user.IsPremium)
                {
                    story.MediaUrl = await _uploadService.UploadVideoAsync(createStoryRequest.ImageFile);
                }
            }
            else if (allowedImageExtensions.Contains(extension))
            {
                story.MediaUrl = await _uploadService.UploadImageAsync(createStoryRequest.ImageFile);
            }
            else
            {
                throw new BadHttpRequestException("Invalid file format");
            }
        }

        story.ExpirationDate = DateTime.UtcNow.AddHours(24);
        story.UserId = user.Id;
        await _unitOfWork.GetRepository<Story>().InsertAsync(story);

        var isSuccess = await _unitOfWork.CommitAsync() > 0;

        if (!isSuccess)
            throw new Exception("Failed to create story");
        var result = _mapper.Map<StoryResponse>(story);
        result.Username = user.Username;
        return result;
    }

    public async Task<List<StoryResponse>> GetStoriesByUserAsync(string username, string? searchTerm)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Unauthorized");
        var user = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username),
            include: x => x.Include(x => x.FriendshipAddressees)
                .Include(x => x.FriendshipRequests)
                .Include(x => x.Stories.Where(x => x.ExpirationDate > DateTime.UtcNow))
        );
        if (user == null)
            throw new AuthenticationException("Unauthorized");

        List<Story> stories = new();
        if (string.IsNullOrEmpty(searchTerm))
        {
            stories.AddRange(user.Stories);
            var friends = await _unitOfWork.GetRepository<Friendship>().GetListAsync(
                predicate: f => (f.RequesterId == user.Id || f.AddresseeId == user.Id) &&
                                f.Status == EFriendshipStatus.Accepted,
                include: f => f.Include(f => f.Requester).Include(f => f.Addressee)
            );
            var friendIds = friends
                .Select(f => f.RequesterId == user.Id ? f.AddresseeId : f.RequesterId)
                .Distinct()
                .ToList();
            var friendStories = await _unitOfWork.GetRepository<Story>().GetListAsync(
                predicate: s => friendIds.Contains(s.UserId) && s.ExpirationDate > DateTime.UtcNow,
                include: s => s.Include(s => s.Member)
            );
            stories.AddRange(friendStories);
        }
        else
        {
            var searchUser = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                predicate: u => u.Username.Equals(searchTerm),
                include: x => x.Include(x => x.Stories.Where(x => x.ExpirationDate > DateTime.UtcNow))
            );
            if (searchUser != null) stories.AddRange(searchUser.Stories);
        }

        stories = stories.OrderByDescending(s => s.CreatedDate).ToList();
        return _mapper.Map<List<StoryResponse>>(stories);
    }

    public async Task<NoContentResult> DeleteStoryAsync(string username, Guid id)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Unauthorized");

        var story = await _unitOfWork.GetRepository<Story>().SingleOrDefaultAsync(
            predicate: x => x.Id.Equals(id) && x.Member.Username.Equals(username)
        );
        _unitOfWork.GetRepository<Story>().DeleteAsync(story);
        var isSuccess = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccess)
            throw new Exception("Failed to delete story");
        return new NoContentResult();
    }

    public async Task<MessageDto> ReplyStoryAsync(string username, CreateMessageDto createMessageDto)
    {
        if (username == createMessageDto.RecipientUsername.ToLower())
            throw new BadHttpRequestException("You cannot send message to yourself");
        var users = await _unitOfWork.GetRepository<Member>().GetListAsync(
            predicate: u => u.Username.Equals(username) || u.Username.Equals(createMessageDto.RecipientUsername)
        );
        var sender = users.SingleOrDefault(x => x.Username.Equals(username));
        var recipient = users.SingleOrDefault(x => x.Username.Equals(createMessageDto.RecipientUsername));
        if (recipient == null) throw new BadHttpRequestException("Not found recipient user");
        var story = await _unitOfWork.GetRepository<Story>().SingleOrDefaultAsync(
            predicate: x => x.Id.Equals(createMessageDto.StoryId) && x.IsDisabled == false
        );
        if (story == null)
            throw new BadHttpRequestException("Story not found");

        var message = new Message
        {
            Id = Guid.NewGuid(),
            MessageDocument = new MessageDocument()
            {
                SenderUsername = sender!.Username,
                RecipientUsername = recipient.Username,
                StoryId = story.Id,
                CreatedDate = DateTime.UtcNow,
                Content = createMessageDto.Content,
            }
        };
        var groupName = GetGroupName(sender.Username, recipient.Username);
        var group = await _unitOfWork.GetRepository<Group>().SingleOrDefaultAsync(
            predicate: x => x.Name == groupName,
            include: x => x.Include(g => g.Connections)
        );
        if (group.Connections.Any(x => x.UserName == recipient.Username))
            message.MessageDocument.DateRead = DateTime.UtcNow;

        await _unitOfWork.GetRepository<Message>().InsertAsync(message);
        await UpdateLastMessageChat(message);
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
                Story = _mapper.Map<StoryResponse>(story)
            };
            messageDto.Story.Username = message.MessageDocument.RecipientUsername;
            await _hubContext.Clients.Group(groupName).SendAsync("NewMessage", messageDto);
            return messageDto;
        }
        else throw new Exception("Failed to send message");
    }

    public async Task<List<StoryByMonthResponse>> GetStoriesByMonthAsync(string username)
    {
        if (username == null)
            throw new AuthenticationException("Unauthorized");
        var member = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username == username
        );
        if (member == null)
            throw new AuthenticationException("Unauthorized");

        var stories = await _unitOfWork.GetRepository<Story>().GetListAsync(
            predicate: x => x.UserId == member.Id,
            include: x => x.Include(x => x.Member)
        );
        var result = stories.GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month })
            .Select(x => new StoryByMonthResponse
            {
                Year = x.Key.Year,
                Month = x.Key.Month,
                Stories = _mapper.Map<List<StoryResponse>>(x.ToList())
            }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
        return result;
    }

    private async Task UpdateLastMessageChat(Message message)
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
                    GroupName = groupName
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

    private string GetGroupName(string caller, string other)
    {
        var stringCompare = string.CompareOrdinal(caller, other) < 0;
        return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
    }
}