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
using OrbitMap.API.Utils;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Domain.Utils;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class StoryService : BaseService<StoryService>, IStoryService
{
    private readonly IUploadService _uploadService;
    private readonly IHubContext<MessageHub> _hubContext;
    private readonly ICraftMyPdfService _craftMyPdfService;

    public StoryService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IUploadService uploadService,
        IHubContext<MessageHub> hubContext, ICraftMyPdfService craftMyPdfService) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _uploadService = uploadService;
        _hubContext = hubContext;
        _craftMyPdfService = craftMyPdfService;
    }

    // public async Task<CreateStoryResponse> CreateStoryAsync(string username, CreateStoryRequest createStoryRequest)
    // {
    //     if (string.IsNullOrEmpty(username))
    //         throw new AuthenticationException("Unauthorized");
    //
    //     var user = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
    //         predicate: x => x.Username.Equals(username)
    //     );
    //     if (user == null)
    //         throw new AuthenticationException("Unauthorized");
    //     var location = await _unitOfWork.GetRepository<Location>().SingleOrDefaultAsync(
    //         predicate: x => x.Name.Trim().ToLower().Equals(createStoryRequest.CityLocation.Trim().ToLower())
    //     );
    //     if (location == null)
    //         throw new BadHttpRequestException("Không thể tìm thấy thành phố trên Việt Nam");
    //     var story = _mapper.Map<Story>(createStoryRequest);
    //     var extension = Path.GetExtension(createStoryRequest.ImageFile.FileName).ToLower();
    //     var allowedImageExtensions = new[] { ".jgeg", ".png", ".jpg", ".gif", ".bmp", ".webp" };
    //     if (createStoryRequest.ImageFile != null)
    //     {
    //         if (extension.Equals(".mp4"))
    //         {
    //             if (user.IsPremium)
    //             {
    //                 story.MediaUrl = await _uploadService.UploadVideoAsync(createStoryRequest.ImageFile);
    //             }
    //         }
    //         else if (allowedImageExtensions.Contains(extension))
    //         {
    //             story.MediaUrl = await _uploadService.UploadImageAsync(createStoryRequest.ImageFile);
    //         }
    //         else
    //         {
    //             throw new BadHttpRequestException("Invalid file format");
    //         }
    //     }
    //
    //     story.ExpirationDate = DateTime.UtcNow.AddHours(24);
    //     story.UserId = user.Id;
    //     await _unitOfWork.GetRepository<Story>().InsertAsync(story);
    //
    //     string? passportImage = null;
    //     var memberLocation = await _unitOfWork.GetRepository<MemberLocation>().SingleOrDefaultAsync(
    //         predicate: ml => ml.LocationId == location.Id && ml.MemberId == user.Id
    //     );
    //     if (memberLocation == null)
    //     {
    //         memberLocation = new MemberLocation
    //         {
    //             Id = Guid.NewGuid(),
    //             LocationId = location.Id,
    //             MemberId = user.Id
    //         };
    //         await _unitOfWork.GetRepository<MemberLocation>().InsertAsync(memberLocation);
    //         passportImage = await _craftMyPdfService.GeneratePassport(user, location);
    //         if (string.IsNullOrEmpty(passportImage)) throw new Exception("Lỗi khi tạo Passport");
    //     }
    //
    //     var isSuccess = await _unitOfWork.CommitAsync() > 0;
    //
    //     if (!isSuccess)
    //         throw new Exception("Failed to create story");
    //     var result = _mapper.Map<CreateStoryResponse>(story);
    //     result.Username = user.Username;
    //     result.AvatarUrl = user.AvatarUrl;
    //     result.PassportImage = passportImage;
    //     return result;
    // }
    public async Task<CreateStoryResponse> CreateStoryAsync(string username, CreateStoryRequest createStoryRequest)
    {
        if (string.IsNullOrEmpty(username))
            throw new AuthenticationException("Unauthorized");

        var user = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        if (user == null)
            throw new AuthenticationException("Unauthorized");
        int nonEmptyFields = 0;
        if (!string.IsNullOrWhiteSpace(createStoryRequest.Location))
            nonEmptyFields++;
        if (!string.IsNullOrWhiteSpace(createStoryRequest.Content))
            nonEmptyFields++;
        if (!string.IsNullOrWhiteSpace(createStoryRequest.Weather))
            nonEmptyFields++;
        // if (!string.IsNullOrWhiteSpace(createStoryRequest.Time))
        //     nonEmptyFields++;
        if (nonEmptyFields != 1 && nonEmptyFields != 0)
        {
            throw new BadHttpRequestException("Chỉ một trong bốn mục Location, Content, Weather hoặc Time có giá trị.");
        }
        // var fields = new[]
        // {
        //     createStoryRequest.Location, createStoryRequest.Content, createStoryRequest.Weather, createStoryRequest.Time
        // };
        // if (fields.Count(string.IsNullOrWhiteSpace) != 1 && fields.Count(string.IsNullOrWhiteSpace) != 0)
        // {
        //     throw new BadHttpRequestException("Chỉ một trong ba mục Location, Content, Weather có giá trị.");
        // }

        var location = await _unitOfWork.GetRepository<Location>().SingleOrDefaultAsync(
            predicate: x => x.Name.Trim().ToLower().Equals(createStoryRequest.CityLocation.Trim().ToLower())
        );
        if (location == null)
            throw new BadHttpRequestException("Không thể tìm thấy thành phố trên Việt Nam");

        var story = _mapper.Map<Story>(createStoryRequest);
        Task<string>? mediaUploadTask = null;
        if (createStoryRequest.ImageFile != null)
        {
            var extension = Path.GetExtension(createStoryRequest.ImageFile.FileName).ToLower();
            var allowedImageExtensions = new[] { ".jgeg", ".png", ".jpg", ".gif", ".bmp", ".webp" };
            if (extension.Equals(".mp4"))
            {
                if (user.IsPremium)
                {
                    mediaUploadTask = _uploadService.UploadVideoAsync(createStoryRequest.ImageFile, true);
                }
            }
            else if (allowedImageExtensions.Contains(extension))
            {
                mediaUploadTask = _uploadService.UploadImageAsync(createStoryRequest.ImageFile);
            }
            else
            {
                throw new BadHttpRequestException("Invalid file format");
            }
        }

        var memberLocationTask = _unitOfWork.GetRepository<MemberLocation>().SingleOrDefaultAsync(
            predicate: ml => ml.LocationId == location.Id && ml.MemberId == user.Id
        );
        if (mediaUploadTask != null)
        {
            story.MediaUrl = await mediaUploadTask;
        }

        story.ExpirationDate = TimeUtil.GetCurrentSEATime().AddHours(24);
        story.UserId = user.Id;
        await _unitOfWork.GetRepository<Story>().InsertAsync(story);

        string? passportImage = null;
        var memberLocation = await memberLocationTask;
        if (memberLocation == null)
        {
            if (location.Id.Equals("vnHN") || location.Id.Equals("vn15"))
            {
                var hanoiMemberLocation = new List<MemberLocation>()
                {
                    new MemberLocation
                    {
                        Id = Guid.NewGuid(),
                        LocationId = "vnHN",
                        MemberId = user.Id
                    },
                    new MemberLocation
                    {
                        Id = Guid.NewGuid(),
                        LocationId = "vn15",
                        MemberId = user.Id
                    }
                };
                await _unitOfWork.GetRepository<MemberLocation>().InsertRangeAsync(hanoiMemberLocation);
            }
            else
            {
                memberLocation = new MemberLocation
                {
                    Id = Guid.NewGuid(),
                    LocationId = location.Id,
                    MemberId = user.Id
                };
                await _unitOfWork.GetRepository<MemberLocation>().InsertAsync(memberLocation);
            }

            passportImage = await _craftMyPdfService.GeneratePassport(user, location);
            if (string.IsNullOrEmpty(passportImage)) throw new Exception("Lỗi khi tạo Passport");
        }

        var isSuccess = await _unitOfWork.CommitAsync() > 0;

        if (!isSuccess)
            throw new Exception("Failed to create story");
        var result = _mapper.Map<CreateStoryResponse>(story);
        result.Username = user.Username;
        result.AvatarUrl = user.AvatarUrl;
        result.PassportImage = passportImage;
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
                .Include(x => x.Stories.Where(x => x.ExpirationDate > TimeUtil.GetCurrentSEATime()))
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
                predicate: s => friendIds.Contains(s.UserId) && s.ExpirationDate > TimeUtil.GetCurrentSEATime(),
                include: s => s.Include(s => s.Member)
            );
            stories.AddRange(friendStories);
        }
        else
        {
            var searchUser = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                predicate: u => u.Username.Equals(searchTerm),
                include: x => x.Include(x => x.Stories.Where(x => x.ExpirationDate > TimeUtil.GetCurrentSEATime()))
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
                CreatedDate = TimeUtil.GetCurrentSEATime(),
                Content = createMessageDto.Content,
                IsSticker = false
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
                IsSticker = message.MessageDocument.IsSticker,
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
        var storyResponses = new List<StoryResponse>();
        foreach (var story in stories)
        {
            var storyResponse = _mapper.Map<StoryResponse>(story);
            // Call helper method to get the resized image as a Base64 data URL.
            // storyResponse.MediaUrl = await ImageUtil.ResizeImage(story.MediaUrl);
            storyResponse.MediaUrl = story.MediaUrl;
            storyResponses.Add(storyResponse);
        }

        var result = storyResponses.GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month })
            .Select(x => new StoryByMonthResponse
            {
                Year = x.Key.Year,
                Month = x.Key.Month,
                Stories = x.ToList()
            }).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
        return result;
    }

    public async Task<List<string>> GetAllImageUrlStoryAsync(string username)
    {
        var stories = await _unitOfWork.GetRepository<Story>().GetListAsync(
            predicate: x => x.Member.Username == username,
            include: x => x.Include(x => x.Member)
        );

        if (stories.Any())
        {
            var mediaUrls = stories.Select(x => x.MediaUrl).ToList();
            return mediaUrls;
        }

        return new List<string>();
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