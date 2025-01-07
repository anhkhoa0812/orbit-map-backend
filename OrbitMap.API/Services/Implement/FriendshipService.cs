using System.Security.Authentication;
using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.Friendship;
using OrbitMap.API.Payload.Response.Friendship;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.API.Services.Interface;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Paginate;
using OrbitMap.Domain.Paginate.Interfaces;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.Services.Implement;

public class FriendshipService : BaseService<FriendshipService>, IFriendshipService
{
    public FriendshipService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<FriendResponse> AddFriendAsync(AddFriendRequest request)
    {
        var requesterUsername = _httpContextAccessor.HttpContext?.User.GetUsername();
        if(string.IsNullOrEmpty(requesterUsername)) 
            throw new AuthenticationException("User is not authenticated");
        if(requesterUsername == request.Username)
            throw new BadHttpRequestException("You cannot add yourself as a friend");
        
        var requester = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Username == requesterUsername);
        var addressee = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Username == request.Username);
        
        if(addressee == null)
            throw new BadHttpRequestException("User not found");
        
        var existFriendship = await _unitOfWork.GetRepository<Friendship>().SingleOrDefaultAsync(predicate: f => (f.RequesterId == requester.Id && f.AddresseeId == addressee.Id) || (f.RequesterId == addressee.Id && f.AddresseeId == requester.Id));
        if(existFriendship != null)
            throw new BadHttpRequestException("Friendship already exists");

        var friendship = new Friendship()
        {
            AddresseeId = addressee.Id,
            RequesterId = requester.Id,
            Status = EFriendshipStatus.Pending
        };
        await _unitOfWork.GetRepository<Friendship>().InsertAsync(friendship);
        var isSuccessful = await _unitOfWork.CommitAsync() > 0;
        if(!isSuccessful)
            throw new Exception("Failed to add friend");
        var result = _mapper.Map<FriendResponse>(friendship);
        var messageSend = $"😊 {requester.DisplayName}  send a friend request to you";
        BackgroundJob.Enqueue<NotificationService>(
            service => service.SendNotificationToUser(requester.DisplayName, addressee.Username, messageSend)
        );
        return result;
    }

    public async Task<FriendResponse> UpdateFriendStatus(UpdateFriendStatusRequest request)
    {
        var addresseeUsername = _httpContextAccessor.HttpContext?.User.GetUsername();
        if(string.IsNullOrEmpty(addresseeUsername)) 
            throw new AuthenticationException("User is not authenticated");
        
        if(request.Status == EFriendshipStatus.Pending)
            throw new BadHttpRequestException("Invalid status");
        
        var addressee = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Username == addresseeUsername);
        var requester = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Username == request.RequestUsername);
        
        if(requester == null)
            throw new BadHttpRequestException("User not found");

        var friendship = await _unitOfWork.GetRepository<Friendship>().SingleOrDefaultAsync(
            predicate: f => (f.RequesterId == requester.Id && f.AddresseeId == addressee.Id) ||
                            (f.RequesterId == addressee.Id && f.AddresseeId == requester.Id)
        );
        if(friendship == null)
            throw new BadHttpRequestException("Friendship not found");
        if(friendship.Status != EFriendshipStatus.Pending)
            throw new BadHttpRequestException("Friendship status is not pending");
        
        friendship.Status = request.Status;
        _unitOfWork.GetRepository<Friendship>().UpdateAsync(friendship);
        
        var isSuccessful = await _unitOfWork.CommitAsync() > 0;
        if(!isSuccessful)
            throw new Exception("Failed to update friend status");
        var result = _mapper.Map<FriendResponse>(friendship);
        return result;
    }

        public async Task<IPaginate<UserDto>> GetFriendsForUser(int page, int size, string? searchTerm, string status)
        {
            if(status != "Accepted" && status != "Pending")
                throw new BadHttpRequestException("Invalid status");
            var currentUsername = _httpContextAccessor.HttpContext?.User.GetUsername();
            if(string.IsNullOrEmpty(currentUsername))
                throw new AuthenticationException("User is not authenticated");
            
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Username == currentUsername);
            if(user == null)
                throw new BadHttpRequestException("User not found");
            var friendships = await _unitOfWork.GetRepository<Friendship>().GetPagingListAsync(
                predicate: f => (f.Requester.Username == currentUsername || f.Addressee.Username == currentUsername) 
                                && f.Status == (status == "Accepted" ? EFriendshipStatus.Accepted : EFriendshipStatus.Pending),
                include: f => f.Include(f => f.Requester).Include(f => f.Addressee),
                page: page,
                size: size,
                orderBy: x => x.OrderBy(x => x.LastModifiedDate ?? x.CreatedDate)
            );
            var friends = friendships.Items.Select(f => f.Requester.Username == currentUsername ? f.Addressee : f.Requester);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerSearchTerm = searchTerm.Trim().ToLower();
                friends = friends.Where(x => x.Username.ToLower().Equals(lowerSearchTerm) ||
                                   x.DisplayName.ToLower().Contains(lowerSearchTerm)
                );
            }
            var paginatedFriends = new Paginate<User>(
                friends, page, size, firstPage: 1
            );
            var result = _mapper.Map<IPaginate<UserDto>>(paginatedFriends);
            return result;
        }
}