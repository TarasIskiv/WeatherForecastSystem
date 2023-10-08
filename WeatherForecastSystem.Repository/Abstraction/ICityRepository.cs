using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Repository.Abstraction;

public interface ICityRepository
{
    Task CreateCity(string cityName);
    Task RemoveCity(int id);
    Task UpdateCity(int id,string cityName);
    Task<bool> VerifyIfCityExists(string cityName);
    Task<List<City>> GetCities();
}