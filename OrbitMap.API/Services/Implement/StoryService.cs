using System.Security.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Payload.Request.Story;
using OrbitMap.API.Payload.Response.Story;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class StoryService : BaseService<StoryService>, IStoryService
{
    private readonly IUploadService _uploadService;

    public StoryService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, IUploadService uploadService) : base(unitOfWork, logger, mapper,
        httpContextAccessor)
    {
        _uploadService = uploadService;
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
        if (!string.IsNullOrEmpty(createStoryRequest.ImageBase64))
        {
            var mediaUrl = await _uploadService.UploadImageAsync(createStoryRequest.ImageBase64);
            story.MediaUrl = mediaUrl.SecureUrl.ToString();
        }

        story.ExpirationDate = DateTime.Now.AddHours(24);
        story.UserId = user.Id;
        await _unitOfWork.GetRepository<Story>().InsertAsync(story);

        var isSuccess = await _unitOfWork.CommitAsync() > 0;

        if (!isSuccess)
            throw new Exception("Failed to create story");
        return _mapper.Map<StoryResponse>(story);
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
}