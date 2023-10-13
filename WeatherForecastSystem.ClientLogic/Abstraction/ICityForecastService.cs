using WeatherForecastSystem.Core.ClientModels;

namespace WeatherForecastSystem.ClientLogic.Abstraction;

public interface ICityForecastService
{
    Task<List<CityForecastClient>> GetCityForecast(string cityName);
}