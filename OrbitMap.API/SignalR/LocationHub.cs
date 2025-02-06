using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.Location;
using OrbitMap.API.Payload.Response.Location;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.SignalR;

[Authorize]
public class LocationHub : Hub
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly LocationTracker _tracker;
    private readonly IUnitOfWork<OrbitMapContext> _unitOfWork;

    public LocationHub(
        LocationTracker tracker,
        IUnitOfWork<OrbitMapContext> unitOfWork,
        IMapper mapper,
        ILogger logger)
    {
        _tracker = tracker;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        var username = Context.User.GetUsername();

        var friends = await GetFriends(username);
        var friendUsernames = friends.Select(f => f.Username).ToList();

        var locations = _tracker.GetLocationsForUsers(friendUsernames);
        if (locations.Any())
        {
            await Clients.Caller.SendAsync("ReceiveInitialLocations", locations);
        }
    }
    

    public async Task UpdateUserLocation(UpdateUserLocationRequest request)
    {
        try
        {
            var username = Context.User.GetUsername();
            var userEntity = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                predicate: x => x.Username.Equals(username)
            );
            if (userEntity == null)
            {
                _logger.Warning("User {Username} not found while updating location.", username);
                return;
            }

            var friends = await GetFriends(username);
            var friendUsernames = friends.Select(f => f.Username).ToList();
            if (!friendUsernames.Any())
            {
                _logger.Information("User {Username} has no friends to send location.", username);
                return;
            }

            var userLocation = _mapper.Map<UserLocationDto>(userEntity);
            userLocation.Latitude = request.Latitude;
            userLocation.Longitude = request.Longitude;
            userLocation.Timestamp = DateTime.UtcNow;
            await Clients.Users(friendUsernames).SendAsync("ReceiveUserLocation", userLocation);
            _logger.Information("Location update sent from {Username} to {FriendCount} friends.",
                username, friendUsernames.Count);
            _tracker.UpdateUserLocation(username, userLocation);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to update location for {Username}", Context.User.GetUsername());
            throw new HubException("Failed to update location: " + e);
        }
    }

    private async Task<List<UserDto>> GetFriends(string currentUsername)
    {
        // Lấy thông tin người dùng hiện tại
        var currentUser = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: u => u.Username == currentUsername
        );

        if (currentUser == null) return new List<UserDto>();

        // Lấy danh sách bạn bè của người dùng hiện tại
        var friends = await _unitOfWork.GetRepository<Friendship>().GetListAsync(
            predicate: f => (f.RequesterId == currentUser.Id || f.AddresseeId == currentUser.Id) &&
                            f.Status == EFriendshipStatus.Accepted,
            include: f => f.Include(f => f.Requester).Include(f => f.Addressee)
        );
        var friendEntities = friends.Select(f => f.RequesterId == currentUser.Id ? f.Addressee : f.Requester).ToList();
        var result = _mapper.Map<List<UserDto>>(friendEntities);
        return result;
    }
    // private async Task<List<UserDto>> GetUsersOnlineAsync(string currentUsername, string[] userOnline)
    // {
    //     // Lấy thông tin người dùng hiện tại
    //     var currentUser = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
    //         predicate: u => u.Username == currentUsername
    //     );
    //
    //     if (currentUser == null) return new List<UserDto>();
    //
    //     // Lấy danh sách bạn bè của người dùng hiện tại
    //     var friends = await _unitOfWork.GetRepository<Friendship>().GetListAsync(
    //         predicate: f => (f.RequesterId == currentUser.Id || f.AddresseeId == currentUser.Id) &&
    //                         f.Status == EFriendshipStatus.Accepted,
    //         include: f => f.Include(f => f.Requester).Include(f => f.Addressee)
    //     );
    //
    //     // Lấy tất cả người dùng online một lần
    //     var userEntities = await _unitOfWork.GetRepository<Member>().GetListAsync(
    //         predicate: u => userOnline.Contains(u.Username)
    //     );
    //
    //     // Lọc người dùng online mà là bạn bè của người dùng hiện tại
    //     var listUserOnline = userEntities
    //         .Where(userEntity =>
    //             friends.Any(f => f.RequesterId == userEntity.Id || f.AddresseeId == userEntity.Id) &&
    //             userEntity.Username != currentUsername).ToList();
    //     var result = _mapper.Map<List<UserDto>>(listUserOnline);
    //     return result;
    // }
}