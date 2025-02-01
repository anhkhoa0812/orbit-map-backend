using OrbitMap.API.Services.Interface;
using StackExchange.Redis;

namespace OrbitMap.API.Services.Implement;

public class RedisService : IRedisService
{
    private readonly IDatabase _db;

    public RedisService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task<string> GetStringAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }

    public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
    {
        return await _db.StringSetAsync(key, value, expiry);
    }

    public async Task<bool> KeyExistsAsync(string key)
    {
        return await _db.KeyExistsAsync(key);
    }

    public async Task<bool> RemoveKeyAsync(string key)
    {
        return await _db.KeyDeleteAsync(key);
    }

    public async Task PushToListAsync(string key, string value)
    {
        await _db.ListRightPushAsync(key, value);
    }

    public async Task RemoveFromListAsync(string key, string value)
    {
        await _db.ListRemoveAsync(key, value);
    }

    public Task<List<string>> GetListAsync(string key)
    {
        return _db.ListRangeAsync(key).ContinueWith(t => t.Result.Select(x => x.ToString()).ToList());
    }

    public async Task<bool> SetHashAsync(string key, string field, string value)
    {
        return await _db.HashSetAsync(key, field, value);
    }

    public async Task<HashEntry[]> GetHashAsync(string key)
    {
        return await _db.HashGetAllAsync(key);
    }

    public async Task RemoveHashAsync(string key, string field)
    {
        await _db.HashDeleteAsync(key, field);
    }
}