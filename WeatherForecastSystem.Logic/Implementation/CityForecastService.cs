using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.Logic.Abstraction;
using WeatherForecastSystem.Repository.Abstraction;

namespace WeatherForecastSystem.Logic.Implementation;

public class CityForecastService : ICityForecastService
{
    private readonly ICityForecastRepository _cityForecastRepository;

    public CityForecastService(ICityForecastRepository cityForecastRepository)
    {
        _cityForecastRepository = cityForecastRepository;
    }
    public async Task Update(List<CityForecast> forecasts, int cityId)
    {
        await _cityForecastRepository.RemoveForecastForCity(cityId);
        await _cityForecastRepository.AddForecastForCities(forecasts);
    }

    public async Task<List<CityForecast>> GetCityForecast(int cityId)
    {
        return await _cityForecastRepository.GetForecastsForCity(cityId);
    }
}