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
    public FriendshipService(IUnitOfWork<OrbitMapContext> unitOfWork, ILogger logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }

    public async Task<List<FriendResponse>> AddFriendAsync(AddFriendRequest request)
    {
        var requesterUsername = _httpContextAccessor.HttpContext?.User.GetUsername();
        if (string.IsNullOrEmpty(requesterUsername))
            throw new AuthenticationException("User is not authenticated");
        if (request.Usernames.Contains(requesterUsername, StringComparer.OrdinalIgnoreCase))
            throw new BadHttpRequestException("You cannot add yourself as a friend");

        var requester = await _unitOfWork.GetRepository<Member>()
            .SingleOrDefaultAsync(predicate: u => u.Username == requesterUsername);
        var response = new List<FriendResponse>();
        foreach (var username in request.Usernames)
        {
            if (username.Equals(requesterUsername, StringComparison.OrdinalIgnoreCase))
                continue;
            var addressee = await _unitOfWork.GetRepository<Member>()
                .SingleOrDefaultAsync(predicate: u => u.Username == username);
            if (addressee == null)
                throw new BadHttpRequestException($"Không tìm thấy người dùng với username: {username}");

            var existFriendship = await _unitOfWork.GetRepository<Friendship>()
                .SingleOrDefaultAsync(
                    predicate: f =>
                        (f.RequesterId == requester.Id && f.AddresseeId == addressee.Id) ||
                        (f.RequesterId == addressee.Id && f.AddresseeId == requester.Id));
            if (existFriendship != null)
            {
                continue;
            }

            var friendship = new Friendship
            {
                AddresseeId = addressee.Id,
                RequesterId = requester.Id,
                Status = EFriendshipStatus.Pending
            };

            await _unitOfWork.GetRepository<Friendship>().InsertAsync(friendship);
            var friendResponse = _mapper.Map<FriendResponse>(friendship);
            response.Add(friendResponse);
            var messageSend = $"{requester.DisplayName}  send a friend request to you";
            BackgroundJob.Enqueue<NotificationService>(
                service => service.SendNotificationToUser(requester.DisplayName, addressee.Username, messageSend)
            );
        }

        var isSuccessful = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccessful)
            throw new Exception("Failed to add friend");
        return response;
    }

    public async Task<FriendResponse> UpdateFriendStatus(UpdateFriendStatusRequest request)
    {
        var addresseeUsername = _httpContextAccessor.HttpContext?.User.GetUsername();
        if (string.IsNullOrEmpty(addresseeUsername))
            throw new AuthenticationException("User is not authenticated");

        if (request.Status == EFriendshipStatus.Pending)
            throw new BadHttpRequestException("Invalid status");

        var addressee = await _unitOfWork.GetRepository<Member>()
            .SingleOrDefaultAsync(predicate: u => u.Username == addresseeUsername);
        var requester = await _unitOfWork.GetRepository<Member>()
            .SingleOrDefaultAsync(predicate: u => u.Username == request.RequestUsername);

        if (requester == null)
            throw new BadHttpRequestException("User not found");

        var friendship = await _unitOfWork.GetRepository<Friendship>().SingleOrDefaultAsync(
            predicate: f => (f.RequesterId == requester.Id && f.AddresseeId == addressee.Id) ||
                            (f.RequesterId == addressee.Id && f.AddresseeId == requester.Id)
        );
        if (friendship == null)
            throw new BadHttpRequestException("Friendship not found");
        if (friendship.Status != EFriendshipStatus.Pending)
            throw new BadHttpRequestException("Friendship status is not pending");

        if (request.Status == EFriendshipStatus.Accepted)
        {
            friendship.Status = request.Status;
            _unitOfWork.GetRepository<Friendship>().UpdateAsync(friendship);
        }

        if (request.Status == EFriendshipStatus.Rejected)
        {
            _unitOfWork.GetRepository<Friendship>().DeleteAsync(friendship);
        }

        var isSuccessful = await _unitOfWork.CommitAsync() > 0;
        if (!isSuccessful)
            throw new Exception("Failed to update friend status");
        var result = _mapper.Map<FriendResponse>(friendship);
        return result;
    }

    public async Task<IPaginate<FriendWithUserResponse>> GetFriendsForUser(int page, int size, string? searchTerm,
        string status)
    {
        if (status != "Accepted" && status != "Pending")
            throw new BadHttpRequestException("Invalid status");
        var currentUsername = _httpContextAccessor.HttpContext?.User.GetUsername();
        if (string.IsNullOrEmpty(currentUsername))
            throw new AuthenticationException("User is not authenticated");

        var user = await _unitOfWork.GetRepository<Member>()
            .SingleOrDefaultAsync(predicate: u => u.Username == currentUsername);
        if (user == null)
            throw new BadHttpRequestException("User not found");
        var friendships = await _unitOfWork.GetRepository<Friendship>().GetPagingListAsync(
            f => (f.Requester.Username == currentUsername || f.Addressee.Username == currentUsername)
                 && f.Status == (status == "Accepted" ? EFriendshipStatus.Accepted : EFriendshipStatus.Pending),
            include: f => f.Include(f => f.Requester).Include(f => f.Addressee),
            page: page,
            size: size,
            orderBy: x => x.OrderBy(x => x.LastModifiedDate ?? x.CreatedDate)
        );

        var friendWithUserResponses = friendships.Items.Select(f =>
        {
            var friendMember = f.Requester.Username == currentUsername ? f.Addressee : f.Requester;
            var response = _mapper.Map<FriendWithUserResponse>(friendMember);
            // Map additional friendship properties.
            response.RequesterUsername = f.Requester.Username;
            response.AddresseeUsername = f.Addressee.Username;
            response.Status = f.Status;

            return response;
        }).ToList();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            var lowerSearchTerm = searchTerm.Trim().ToLower();
            friendWithUserResponses = friendWithUserResponses.Where(x => x.Username.ToLower().Equals(lowerSearchTerm) ||
                                                                         x.DisplayName.ToLower()
                                                                             .Contains(lowerSearchTerm)
            ).ToList();
        }

        var result = new Paginate<FriendWithUserResponse>(
            friendWithUserResponses, page, size, 1
        );
        // var result = _mapper.Map<IPaginate<UserDto>>(paginatedFriends);
        return result;
    }

    public async Task<IPaginate<FriendWithUserResponse>> GetRecommendFriendsForUser(string username, int page, int size,
        List<string> phoneNumbers)
    {
        _logger.Information("phoneNumbers: {phoneNumbers}", phoneNumbers);
        if (username == null) throw new ArgumentNullException(nameof(username));
        if (!phoneNumbers.Any())
            throw new ArgumentNullException(nameof(phoneNumbers));
        var currentUser = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: u => u.Username == username
        );
        if (currentUser == null)
            throw new Exception("User not found.");

        var friends = await _unitOfWork.GetRepository<Friendship>().GetListAsync(
            predicate: f => (f.RequesterId == currentUser.Id || f.AddresseeId == currentUser.Id),
            include:
            f => f.Include(f => f.Requester)
                .Include(f => f.Addressee)
        );

        var users = await _unitOfWork.GetRepository<Member>().GetPagingListAsync(
            predicate: m =>
                phoneNumbers.Contains(m.PhoneNumber) && m.Username != username,
            page: page,
            size: size
        );
        var friendWithUsersResponse = users.Items.Select(member =>
        {
            var friendship = friends.FirstOrDefault(f =>
                f.Requester.Username == member.Username || f.Addressee.Username == member.Username);

            var response = _mapper.Map<FriendWithUserResponse>(member);
            if (friendship != null)
            {
                response.AddresseeUsername = friendship.Addressee.Username;
                response.RequesterUsername = friendship.Requester.Username;
                response.Status = friendship.Status;
            }

            return response;
        }).ToList();
        var result = new Paginate<FriendWithUserResponse>(
            friendWithUsersResponse, page, size, 1
        );
        return result;
    }
}