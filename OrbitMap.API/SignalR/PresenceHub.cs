using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OrbitMap.API.Helper;
using OrbitMap.API.Payload.Request.User;
using OrbitMap.API.Payload.Response.Location;
using OrbitMap.API.Payload.Response.User;
using OrbitMap.Domain.Entities;
using OrbitMap.Domain.Enums;
using OrbitMap.Domain.Persistent;
using OrbitMap.Repository.Interfaces;
using ILogger = Serilog.ILogger;

namespace OrbitMap.API.SignalR;

[Authorize]
public class PresenceHub : Hub
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly PresenceTracker _tracker;
    private readonly IUnitOfWork<OrbitMapContext> _unitOfWork;

    public PresenceHub(PresenceTracker tracker, IUnitOfWork<OrbitMapContext> unitOfWork, IMapper mapper, ILogger logger)
    {
        _tracker = tracker;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var username = Context.User.GetUsername();
        var isOnline =
            await _tracker.UserConnected(username, Context.ConnectionId); //Cập nhập trạng thái online cho user
        if (isOnline)
        {
            //Nếu người dùng online lần đầu thì gửi thông báo cho các người dùng khác
            var userEntity = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                predicate: x => x.Username.Equals(username)
            );
            var user = _mapper.Map<UserDto>(userEntity);
            var friendUsernames = await GetFriendUserNameOfUserAsync(userEntity);
            foreach (var friendUsername in friendUsernames)
            {
                var connectionId = await _tracker.GetConnectionsForUser(friendUsername);
                if (connectionId != null) await Clients.Clients(connectionId).SendAsync("UserIsOnline", user);
            }
            // var user = _mapper.Map<UserDto>(userEntity);
            // await Clients.Others.SendAsync("UserIsOnline", user);
        }

        //Gửi danh sách bạn bè đang online cho user hiện tại
        var currentUsers = await _tracker.GetOnlineUsers();
        var usersOnline = await GetUsersOnlineAsync(username, currentUsers);
        _logger.Information($"User: {username} with {usersOnline.Count} friends online");
        await Clients.Caller.SendAsync("GetOnlineUsers", usersOnline);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var username = Context.User.GetUsername();
        var isOffline = await _tracker.UserDisconnected(username, Context.ConnectionId); //Cập nhập trạng thái offline
        if (isOffline)
        {
            //Nếu người dùng chuyển từ online sang offline, thông báo đến client khác
            // await Clients.Others.SendAsync("UserIsOffline", username);
            var userEntity = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
                predicate: x => x.Username.Equals(username)
            );
            var friendUsernames = await GetFriendUserNameOfUserAsync(userEntity);
            foreach (var friendUsername in friendUsernames)
            {
                var connectionId = await _tracker.GetConnectionsForUser(friendUsername);
                if (connectionId != null) await Clients.Clients(connectionId).SendAsync("UserIsOffline", username);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    //Sử dụng để chia sẻ thông tin giữa các clients (Chia sẻ trạng thái hoặc vị trí)
    public async Task UpdateUserPeer(UserPeer userPeer)
    {
        var userEntity = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(Context.User.GetUsername())
        );
        var user = _mapper.Map<UserDto>(userEntity);
        userPeer.Member = user;
        await Clients.All.SendAsync("OnUpdateUserPeer", userPeer);
    }

    public async Task UpdateUserLocation(double latitude, double longitude)
    {
        var username = Context.User.GetUsername();
        var userEntity = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
            predicate: x => x.Username.Equals(username)
        );
        var user = _mapper.Map<UserDto>(userEntity);
        var userLocation = new UserLocationDto
        {
            Username = username,
            Latitude = latitude,
            Longitude = longitude,
            Timestamp = DateTime.UtcNow
        };
        var onlineFriends = await GetUsersOnlineAsync(username, await _tracker.GetOnlineUsers());
        foreach (var friend in onlineFriends)
        {
            var connectionId = await _tracker.GetConnectionsForUser(friend.Username);
            if (connectionId != null)
                await Clients.Clients(connectionId).SendAsync("ReceiveUserLocation", user, userLocation);
        }
    }

    private async Task<List<UserDto>> GetUsersOnlineAsync(string currentUsername, string[] userOnline)
    {
        // var listUserOnline = new List<UserDto>();
        // foreach (var u in userOnline)
        // {
        //     var userEntity = await _unitOfWork.GetRepository<Member>().SingleOrDefaultAsync(
        //         predicate: x => x.Username.Equals(u)
        //     );
        //     var user = _mapper.Map<UserDto>(userEntity);
        //     listUserOnline.Add(user);
        //     _logger.Information($"User1: {user.Username}");
        // }
        // return await Task.Run(() => listUserOnline.Where(x => x.Username != currentUsername).ToList());
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

        // Lấy tất cả người dùng online một lần
        var userEntities = await _unitOfWork.GetRepository<Member>().GetListAsync(
            predicate: u => userOnline.Contains(u.Username)
        );

        // Lọc người dùng online mà là bạn bè của người dùng hiện tại
        var listUserOnline = userEntities
            .Where(userEntity =>
                friends.Any(f => f.RequesterId == userEntity.Id || f.AddresseeId == userEntity.Id) &&
                userEntity.Username != currentUsername).ToList();
        var result = _mapper.Map<List<UserDto>>(listUserOnline);
        return result;
    }

    private async Task<List<string>> GetFriendUserNameOfUserAsync(Member user)
    {
        var friends = await _unitOfWork.GetRepository<Friendship>().GetListAsync(
            predicate: f => (f.RequesterId == user.Id || f.AddresseeId == user.Id) &&
                            f.Status == EFriendshipStatus.Accepted,
            include: f => f.Include(f => f.Requester).Include(f => f.Addressee)
        );
        var friendUsernames = friends
            .Select(f => f.RequesterId == user.Id ? f.Addressee.Username : f.Requester.Username).ToList();
        return friendUsernames;
    }
}