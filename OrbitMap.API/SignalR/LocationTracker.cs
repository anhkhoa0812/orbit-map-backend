using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using OrbitMap.API.Payload.Response.Location;
using OrbitMap.API.Services.Interface;
using StackExchange.Redis;

namespace OrbitMap.API.SignalR;

public class LocationTracker
{
    private static readonly ConcurrentDictionary<string, UserLocationDto> UserLocations = new();
    private readonly IRedisService _redisService;
    private const string LocationPrefixKey = "UserLocation:";

    public LocationTracker(IRedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task UpdateUserLocation(string username, UserLocationDto location)
    {
        // UserLocations.AddOrUpdate(username, location, (key, oldValue) => location);
        var json = JsonSerializer.Serialize(location);
        await _redisService.SetStringAsync($"{LocationPrefixKey}{username}", json);
    }

    private async Task<UserLocationDto?> GetUserLocation(string username)
    {
        // UserLocations.TryGetValue(username, out var location);
        // return location;
        var json = await _redisService.GetStringAsync($"{LocationPrefixKey}{username}");
        return json != null ? JsonSerializer.Deserialize<UserLocationDto>(json) : null;
    }

    public Task<List<UserLocationDto?>> GetLocationsForUsers(List<string> usernames)
    {
        // return usernames.Select(u => GetUserLocation(u)).Where(l => l != null).ToList();
        // var keys = usernames.Select(u => (RedisKey)$"{LocationPrefixKey}{u}").ToArray();
        // var results = await _redisService.GetStringListAsync(keys);
        return Task.FromResult(usernames.Select(u => GetUserLocation(u).Result).Where(l => l != null).ToList());
    }
}