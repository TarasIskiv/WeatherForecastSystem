using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.CustomMapper;
using WeatherForecastSystem.Core.CustomModels;
using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.Logic.Abstraction;
using WeatherForecastSystem.RedisLogic.Abstraction;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;

namespace WeatherForecastSystem.ScrapeDriver.Helpers;

public class ScrapeProcessor
{
    private readonly IRedisService _redisService;
    private readonly IForecastService _forecastService;
    private readonly ICityForecastService _cityForecastService;
    private readonly ICityService _cityService;
    private readonly IServiceBusMessagingService _serviceBusMessagingService;

    public ScrapeProcessor(IRedisService redisService, IForecastService forecastService, ICityForecastService cityForecastService, ICityService cityService, IServiceBusMessagingService serviceBusMessagingService)
    {
        _redisService = redisService;
        _forecastService = forecastService;
        _cityForecastService = cityForecastService;
        _cityService = cityService;
        _serviceBusMessagingService = serviceBusMessagingService;
    }

    public async Task Process()
    {
        var message = await _serviceBusMessagingService.ReceiveMessage<string>();
        var cities = await _cityService.GetCities();
        foreach (var city in cities)
        {
            await SingleAction(city);
        }
        var key = _redisService.GetCityListKey();
        await _redisService.SetData(key, cities);
    }

    private async Task SingleAction(City city)
    {
        var forecasts = await Scrape(city.CityName);
        if(forecasts is null || !forecasts.Any()) return;
        var cityForecasts = forecasts.ToCityForecastList(city.CityId);
        await UpdateForecasts(cityForecasts, city.CityId);
        var dbForecasts = await _cityForecastService.GetCityForecast(city.CityId);
        var key = _redisService.GetKey(city.CityName);
        await _redisService.SetData(key, dbForecasts);
    }
    private async Task<List<Forecast>> Scrape(string cityName)
    {
        return await _forecastService.GetForecast(cityName);
    }

    private async Task UpdateForecasts(List<CityForecast> forecasts, int cityId)
    {
        await _cityForecastService.Update(forecasts, cityId);
    }
}