using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Logic.Abstraction;

public interface ICityForecastService
{
    Task Update(List<CityForecast> forecasts);
    Task<List<CityForecast>> GetCityForecast(int cityId);
}