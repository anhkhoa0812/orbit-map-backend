using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OrbitMap.API.Services.Interface;

namespace OrbitMap.API.SignalR;

[Authorize]
public class DeviceHub : Hub
{
    private static readonly Dictionary<string, Dictionary<string, string>> UserDeviceConnections = new();
    // private readonly IRedisService _redisService;
    //
    // public DeviceHub(IRedisService redisService)
    // {
    //     _redisService = redisService;
    // }

    public override async Task OnConnectedAsync()
    {
        var username = Context.GetHttpContext()?.Request.Query["username"];
        var subscriptionId = Context.GetHttpContext()?.Request.Query["subscriptionId"];

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(subscriptionId))
        {
            throw new HubException("Không tìm thấy thông tin đăng nhập");
        }

        // var connectionKey = $"{username}:{subscriptionId}";
        // await _redisService.PushToListAsync(connectionKey, Context.ConnectionId);

        lock (UserDeviceConnections)
        {
            if (!UserDeviceConnections.ContainsKey(username!))
            {
                UserDeviceConnections[username!] = new Dictionary<string, string>();
            }

            UserDeviceConnections[username!][subscriptionId!] = Context.ConnectionId;
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // var username = Context.GetHttpContext()?.Request.Query["username"];
        // var subscriptionId = Context.GetHttpContext()?.Request.Query["subscriptionId"];
        //
        // if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(subscriptionId))
        // {
        //     var connectionKey = $"{username}:{subscriptionId}";
        //     await _redisService.RemoveFromListAsync(connectionKey, Context.ConnectionId);
        // }
        var username = string.Empty;
        var deviceId = string.Empty;
        lock (UserDeviceConnections)
        {
            username = UserDeviceConnections.FirstOrDefault(x => x.Value.ContainsValue(Context.ConnectionId)).Key;
            if (!string.IsNullOrEmpty(username))
            {
                deviceId = UserDeviceConnections[username!].FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
                UserDeviceConnections[username!].Remove(deviceId!);

                if (UserDeviceConnections[username!].Count == 0)
                {
                    UserDeviceConnections.Remove(username!);
                }
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task ForceLogout(string username, string subscriptionId)
    {
        // var connectionKey = $"{username}:{subscriptionId}";
        // var connectionIds = await _redisService.GetListAsync(connectionKey);
        // if (connectionIds.Count > 0)
        // {
        //     foreach (var connectionId in connectionIds)
        //     {
        //     }
        // }

        if (UserDeviceConnections.ContainsKey(username) && UserDeviceConnections[username].Count > 0)
        {
            foreach (var entry in UserDeviceConnections[username])
            {
                if (entry.Key != subscriptionId)
                {
                    var connectionId = entry.Value;
                    if (!string.IsNullOrEmpty(connectionId))
                    {
                        await Clients.Client(connectionId).SendAsync("Logout");
                    }
                }
            }
        }
    }
}