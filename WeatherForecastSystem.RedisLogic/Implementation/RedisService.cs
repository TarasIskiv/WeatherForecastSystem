using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WeatherForecastSystem.RedisLogic.Abstraction;

namespace WeatherForecastSystem.RedisLogic.Implementation;

public class RedisService : IRedisService
{
    private IDistributedCache _cache;
    private DistributedCacheEntryOptions _options { get; set; } = new();

    public RedisService(IDistributedCache distributedCache)
    {
        _cache = distributedCache;
        _options.AbsoluteExpirationRelativeToNow = new TimeSpan(1, 30, 0);
    }
    public async Task SetData<T>(string key, T data)
    {
        await _cache.RemoveAsync(key);
        var serializedData = JsonSerializer.Serialize(data);
        await _cache.SetStringAsync(key, serializedData, _options);
    }

    public async Task<T> GetData<T>(string key)
    {
        var data = await _cache.GetStringAsync(key);
        var desirializedData = JsonSerializer.Deserialize<T>(data);
        return desirializedData ?? default!;
    }

    public string GetKey(string cityName)
    {
        return $"City/{cityName}";
    }
}