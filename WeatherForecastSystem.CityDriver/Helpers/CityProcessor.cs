using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Logic.Abstraction;
using WeatherForecastSystem.RedisLogic.Abstraction;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;

namespace WeatherForecastSystem.CityDriver.Helpers;

public class CityProcessor
{
    private readonly IServiceBusMessagingService _serviceBusMessagingService;
    private readonly IRedisService _redisService;
    private readonly ICityService _cityService;

    public CityProcessor(IServiceBusMessagingService serviceBusMessagingService, IRedisService redisService, ICityService cityService)
    {
        _serviceBusMessagingService = serviceBusMessagingService;
        _redisService = redisService;
        _cityService = cityService;
    }

    public async Task Process()
    {
        var cityAction = await _serviceBusMessagingService.ReceiveMessage<CityAction>();
        await _cityService.ProcessCityAction(cityAction);
        var cities = await _cityService.GetCities();
        
        var cacheKey = _redisService.GetCityListKey();
        await _redisService.SetData(cacheKey, cities);
    }
}