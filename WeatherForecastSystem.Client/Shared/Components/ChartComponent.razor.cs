using Microsoft.AspNetCore.Components;
using MudBlazor;
using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.Enums;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Client.Shared.Components;

partial class ChartComponent : IDisposable
{
    [Parameter] public List<string> Cities { get; set; } = new();
    [Parameter] public ForecastType Type { get; set; }
    [Inject] public ICityForecastService CityForecastService { get; set; } = default!;
    public List<CityForecastClient> ForecastClients { get; set; } = new();
    public List<ChartSeries> Series = new();
    public bool Refresh { get; set; } = true;
    public string[] XAxisLabels = new []
    {
        DateTime.UtcNow.ToString("dd/MM/yyyy"),
        DateTime.UtcNow.AddDays(1).ToString("dd/MM/yyyy"),
        DateTime.UtcNow.AddDays(2).ToString("dd/MM/yyyy"),
        DateTime.UtcNow.AddDays(3).ToString("dd/MM/yyyy"),
        DateTime.UtcNow.AddDays(4).ToString("dd/MM/yyyy"),
        DateTime.UtcNow.AddDays(5).ToString("dd/MM/yyyy")
    };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender || Refresh)
        {
            Refresh = false;
            await LoadForecasts();
            InitializeChartSeries();
            StateHasChanged();
        }
    }

    private async Task LoadForecasts()
    {
        ForecastClients.Clear();
        foreach (var city in Cities)
        {
            if (ForecastClients.Any(forecast => forecast.CityName.Equals(city)))
            {
                var forecastsToRemove = ForecastClients.Where(forecast => forecast.CityName.Equals(city)).ToList();
                forecastsToRemove.ForEach(forecast => ForecastClients.Remove(forecast));
            }
            var forecast = await CityForecastService.GetCityForecast(city);
            ForecastClients.AddRange(forecast);
        }
    }

    private void InitializeChartSeries()
    {
        Series.Clear();
        foreach (var city in Cities)
        {
            var values = GetChartValues(city);
            Series.Add(new ChartSeries() {Name = city, Data = values});
        }
       // StateHasChanged();
    }

    private double[] GetChartValues(string cityName)
    {
        var selectedForecasts = ForecastClients.Where(forecast => Equals(forecast.CityName, cityName)).ToList();
        if (!selectedForecasts.Any()) return new double[]{};
        var values = new List<double>();

        foreach (var forecast in selectedForecasts)
        {
            var type = forecast.GetType();
            var propertyInfo = type.GetProperty(Type.ToString());
            if (propertyInfo is null) return new double[]{};

            var value = propertyInfo.GetValue(forecast);
            if (Double.TryParse(value?.ToString(), out double parsedValue)) values.Add(parsedValue);
        }

        return values.ToArray();
    }

    public void Dispose()
    {
        Refresh = true;
        Cities.Clear();
        Series.Clear();
        ForecastClients.Clear();
    }
}