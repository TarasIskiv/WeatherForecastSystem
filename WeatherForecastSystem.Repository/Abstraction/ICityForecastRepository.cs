using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Repository.Abstraction;

public interface ICityForecastRepository
{
    Task RemoveForecastForCity(int cityId);
    Task AddForecastForCities(List<CityForecast> forecasts);
    Task<List<CityForecastClient>> GetForecastsForCity(int cityId);
}