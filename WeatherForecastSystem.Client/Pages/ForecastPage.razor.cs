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
    [Inject] private ICityService CityService { get; set; } = default!;
    public List<CityForecastClient> Forecasts { get; set; } = new();
    [AllowNull] private ChartComponent _chart = new ChartComponent();
    public bool Compare { get; set; } = false;
    public List<string> AvailableCities { get; set; } = new();
    private string value { get; set; } = "Nothing selected";
    private IEnumerable<string> _selectedCities { get; set; } = new HashSet<string>() {};

    protected override async Task OnParametersSetAsync()
    {
        if(string.IsNullOrEmpty(City)) return;

        _selectedCities = new HashSet<string>() { City };
        
        _chart.Dispose();
        await LoadCities();
        StateHasChanged();
    }

    public async Task LoadCities()
    {
        var cities = await CityService.GetCities();
        AvailableCities = cities
                            .Where(x => !x.CityName.Equals(City))
                            .Select(x => x.CityName)
                            .ToList();
    }

    public List<string> GetCities() => new List<string>() {City};
    
    private string GetMultiSelectionText(List<string> selectedValues)
    {
        return $"{selectedValues.Count} cit{(selectedValues.Count > 1 ? "ies have":"y has")} been selected";
    }

    private void UpdateChart()
    {
        _chart.Dispose();
        StateHasChanged();
    }

}