using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.Core.ClientModels;

namespace WeatherForecastSystem.Client.Pages;

partial class ForecastPage
{
    [Parameter, AllowNull] public string City { get; set; }
    [Inject] private ICityForecastService CityForecastService { get; set; } = default!;
    public List<CityForecastClient> Forecasts { get; set; } = new();
    protected override async Task OnParametersSetAsync()
    {
        if(string.IsNullOrEmpty(City)) return;
        await LoadForecast();
        StateHasChanged();
    }

    private async Task LoadForecast()
    {
       Forecasts = await CityForecastService.GetCityForecast(City);
    }
}