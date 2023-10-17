using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using WeatherForecastSystem.Client.Shared.Components;
using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Client.Pages;

partial class ForecastPage
{
    [Parameter, AllowNull] public string City { get; set; }
    [Inject] private ICityForecastService CityForecastService { get; set; } = default!;
    public List<CityForecastClient> Forecasts { get; set; } = new();
    [AllowNull] private ChartComponent _chart = new ChartComponent(); 
    protected override async Task OnParametersSetAsync()
    {
        if(string.IsNullOrEmpty(City)) return;
        
         _chart.Dispose();
        await LoadForecast();
        StateHasChanged();
    }

    private async Task LoadForecast()
    {
       Forecasts = await CityForecastService.GetCityForecast(City);
    }

    public List<string> GetCities() => new List<string>() {City};
}