using Microsoft.AspNetCore.Components;
using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Client.Shared.Components;

partial class CityNavMenu
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;
    [Inject] private ICityService CityService { get; set; } = default!;
    private List<City> Cities { get; set; } = new();
    public List<City> UserCities { get; set; } = new();
    public string Search { get; set; } = string.Empty;
    protected override async Task OnInitializedAsync()
    {
        await LoadCities();
        ApplyFilter();
    }

    private async Task LoadCities()
    {
        Cities = await CityService.GetCities();
    }

    public void Navigate(string cityName)
    {
        NavManager.NavigateTo($"Forecast/{cityName}");
    }

    public void ApplyFilter()
    {
        if (string.IsNullOrWhiteSpace(Search))
        {
            UserCities = Cities; return;
        }
        UserCities = Cities.Where(city => city.CityName.Contains(Search)).ToList();
    }
}