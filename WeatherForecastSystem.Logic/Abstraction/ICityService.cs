using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Logic.Abstraction;

public interface ICityService
{
    Task ProcessCityAction(CityAction cityAction);
    Task<List<City>> GetCities();
}