using Microsoft.AspNetCore.Components;
using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Client.Shared.Components;

partial class ChartComponent
{
    [Parameter] public List<City> Cities { get; set; } = new();
    [Inject] public ICityForecastService CityForecastService { get; set; } = default!;
    public List<CityForecastClient> ForecastClients { get; set; } = new();
    
    protected override async Task OnParametersSetAsync()
    {
        await LoadForecasts();
    }

    private async Task LoadForecasts()
    {
        foreach (var city in Cities)
        {
            var forecast = await CityForecastService.GetCityForecast(city.CityName);
            ForecastClients.AddRange(forecast);
        }
    }
}