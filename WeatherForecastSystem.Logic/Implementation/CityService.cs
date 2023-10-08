using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.Enums;
using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.Logic.Abstraction;
using WeatherForecastSystem.Repository.Abstraction;

namespace WeatherForecastSystem.Logic.Implementation;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    private async Task CreateCity(string cityName)
    {
        var cityExists = await _cityRepository.VerifyIfCityExists(cityName);
        if(cityExists) return;
        await _cityRepository.CreateCity(cityName);
    }

    private async Task RemoveCity(int id)
    {
        await _cityRepository.RemoveCity(id);
    }

    private async Task UpdateCity(int id, string cityName)
    {
        await _cityRepository.UpdateCity(id, cityName);
    }

    public async Task ProcessCityAction(CityAction cityAction)
    {
        switch (cityAction.Action)
        {
            case ActionType.Create: await CreateCity(cityAction.SelectedCity.CityName); return;
            case ActionType.Update: await CreateCity(cityAction.SelectedCity.CityName); return;
            case ActionType.Delete: await CreateCity(cityAction.SelectedCity.CityName); return;
        }
    }

    public async Task<List<City>> GetCities()
    {
        return await _cityRepository.GetCities();
    }
}