using Microsoft.AspNetCore.Components;

namespace WeatherForecastSystem.Client.Shared.Components;

partial class CityNavMenu
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;

    public void Navigate(string cityName)
    {
        NavManager.NavigateTo($"Forecast/{cityName}");
    }
}