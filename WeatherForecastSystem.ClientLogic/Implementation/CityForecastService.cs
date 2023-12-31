using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.RedisLogic.Abstraction;
using WeatherForecastSystem.RedisLogic.Implementation;

namespace WeatherForecastSystem.ClientLogic.Implementation;

public class CityForecastService : ICityForecastService
{
    private readonly IRedisService _redisService;

    public CityForecastService(IRedisService redisService)
    {
        _redisService = redisService;
    }
    public async Task<List<CityForecastClient>> GetCityForecast(string cityName)
    {
        var key = _redisService.GetKey(cityName);
        var forecasts = await _redisService.GetData <List<CityForecastClient>>(key);
        return forecasts ?? new();
    }
}