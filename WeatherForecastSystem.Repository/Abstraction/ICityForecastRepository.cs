using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Repository.Abstraction;

public interface ICityForecastRepository
{
    Task RemoveForecastForCities();
    Task AddForecastForCity(List<CityForecast> forecasts);
    Task<List<CityForecast>> GetForecastsForCity(int cityId);
}