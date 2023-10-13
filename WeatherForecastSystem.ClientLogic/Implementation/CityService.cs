using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.RedisLogic.Abstraction;

namespace WeatherForecastSystem.ClientLogic.Implementation;

public class CityService : ICityService
{
    private readonly IRedisService _redisService;

    public CityService(IRedisService redisService)
    {
        _redisService = redisService;
    }
    public async Task<List<City>> GetCities()
    {
        var key = _redisService.GetCityListKey();
        var cities = await _redisService.GetData<List<City>>(key);
        return cities ?? new();
    }
}