using OrbitMap.API.Services.Interface;
using StackExchange.Redis;

namespace OrbitMap.API.Services.Implement;

public class RedisService : IRedisService
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer _redisConnection;

    public RedisService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
        _redisConnection = redis;
    }

    public async Task<string> GetStringAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }

    public async Task<RedisValue[]> GetStringListAsync(RedisKey[] keys)
    {
        return await _db.StringGetAsync(keys);
    }

    public async Task<List<string>> GetListByPatternAsync(string pattern)
    {
        RedisResult result = await _db.ScriptEvaluateAsync(
            "return redis.call('keys', ARGV[1])",
            keys: null,
            values: new RedisValue[] { pattern },
            flags: CommandFlags.None
        );

        // Cast the result to a RedisResult array.
        RedisResult[] keys = (RedisResult[])result;

        var resultList = new List<string>();
        if (keys == null || keys.Length == 0)
            return resultList;

        // Retrieve list items for each key found.
        foreach (var key in keys)
        {
            var listItems = await _db.ListRangeAsync(key.ToString());
            resultList.AddRange(listItems.Select(x => x.ToString()));
        }

        return resultList;
    }

    public async Task<List<string>> GetKeysByPatternAsync(string pattern)
    {
        var endpoints = _redisConnection.GetEndPoints();
        if (!endpoints.Any())
        {
            throw new Exception("No endpoints found in the connection multiplexer.");
        }

        var server = _redisConnection.GetServer(endpoints.First());
        var keys = server.Keys(pattern: pattern);
        return keys.Select(x => x.ToString()).ToList();
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