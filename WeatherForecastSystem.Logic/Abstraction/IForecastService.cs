using WeatherForecastSystem.Core.CustomModels;

namespace WeatherForecastSystem.Logic.Abstraction;

public interface IForecastService
{
    Task<List<Forecast>> GetForecast(string cityName);
}