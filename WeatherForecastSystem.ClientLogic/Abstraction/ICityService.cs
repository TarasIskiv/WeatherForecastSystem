using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.ClientLogic.Abstraction;

public interface ICityService
{
    Task<List<City>> GetCities();
}