using System.Text.Json;
using OrbitMap.API.Payload.Response.Device;
using OrbitMap.API.Services.Interface;

namespace OrbitMap.API.SignalR;

public class DeviceTracker
{
    private readonly IRedisService _redisService;

    public DeviceTracker(IRedisService redisService)
    {
        _redisService = redisService;
    }

    private string GetKey(string username)
    {
        return $"DeviceTracker:{username}";
    }

    public async Task<DeviceInfo> RegisterDeviceAsync(string username, string subscriptionId, string connectionId)
    {
        var key = GetKey(username);

        var previousJson = await _redisService.GetStringAsync(key);


        DeviceInfo previousDevice = null;
        var newDevice = new DeviceInfo()
        {
            ConnectionId = connectionId,
            SubscriptionId = subscriptionId
        };
        if (!string.IsNullOrEmpty(previousJson))
        {
            previousDevice = JsonSerializer.Deserialize<DeviceInfo>(previousJson);
        }
        else
        {
            previousDevice = newDevice;
        }

        var newJson = JsonSerializer.Serialize(newDevice);
        await _redisService.SetStringAsync(key, newJson);

        return previousDevice;
    }

    // public async Task UnregisterDeviceAsync(string username, string subscriptionId, string connectionId)
    // {
    //     var key = GetKey(username, subscriptionId);
    //     var storedConnectionId = await _redisService.GetStringAsync(key);
    //
    //     if (storedConnectionId.Equals(connectionId))
    //     {
    //         await _redisService.RemoveKeyAsync(key);
    //     }
    // }
}