using StackExchange.Redis;

namespace OrbitMap.API.Services.Interface;

public interface IRedisService
{
    Task<string> GetStringAsync(string key);

    Task<RedisValue[]> GetStringListAsync(RedisKey[] keys);

    Task<List<string>> GetListByPatternAsync(string pattern);
    Task<List<string>> GetKeysByPatternAsync(string pattern);
    Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null);
    Task<bool> KeyExistsAsync(string key);
    Task<bool> RemoveKeyAsync(string key);
    Task PushToListAsync(string key, string value);

    Task RemoveFromListAsync(string key, string value);

    Task<List<string>> GetListAsync(string key);
    Task<bool> SetHashAsync(string key, string field, string value);

    Task<HashEntry[]> GetHashAsync(string key);
    Task RemoveHashAsync(string key, string field);
}