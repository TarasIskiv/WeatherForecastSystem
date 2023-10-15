using MediatR;
using Microsoft.AspNetCore.Components;
using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.Enums;
using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.MediatR.Commands;

namespace WeatherForecastSystem.Client.Shared.Components;

partial class CityNavMenu
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;
    [Inject] private ICityService CityService { get; set; } = default!;
    [Inject] private IMediator Mediator { get; set; } = default!;
    private List<City> Cities { get; set; } = new();
    public List<City> UserCities { get; set; } = new();
    public bool IsInEditMode { get; set; } = false;
    public string UpdateCityName { get; set; } = String.Empty;
    public CityAction SelectedCityAction { get; set; } = new CityAction()
        { SelectedCity = null, Action = ActionType.Update };
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

    public void StartUpdate(City city)
    {
        UpdateCityName = city.CityName;
        SelectedCityAction.SelectedCity = city;
        IsInEditMode = true;
        StateHasChanged();
    }

    public async Task FinishUpdating(bool save)
    {
        if (!save || string.IsNullOrWhiteSpace(UpdateCityName))
        {
            if(SelectedCityAction.SelectedCity?.CityId == 0) RemoveNewCity();
            IsInEditMode = false;
            StateHasChanged();
            return;
        }

        var selectedCity = SelectedCityAction.SelectedCity;
        var newCity = new City(selectedCity.CityId, UpdateCityName, true);
        SelectedCityAction.SelectedCity = newCity;
        SelectedCityAction.Action = newCity.CityId == 0 ? ActionType.Create : ActionType.Update;
        var command = new CityActionRequest(SelectedCityAction);
        var isSuccess = await Mediator.Send(command);
        IsInEditMode = false;
        SelectedCityAction.SelectedCity = null;
        if (isSuccess)
            await LoadCities();
    }

    public bool IsCityInEditMode(City city)
    {
        return city.CityId == SelectedCityAction.SelectedCity?.CityId;
    }

    private void RemoveNewCity()
    {
        var city = UserCities.Find(x => x.CityId == 0);
        if(city is null) return;
        UserCities.Remove(city);
    }

    public async Task RemoveCity(City city)
    {
        var cityAction = new CityAction() { SelectedCity = city, Action = ActionType.Delete };
        var isSuccess = await Mediator.Send(new CityActionRequest(SelectedCityAction));
        if (isSuccess) await LoadCities(); 
        StateHasChanged();
    }

    public void AddCity()
    {
        IsInEditMode = true;
        var newCity = new City(0, string.Empty, true);
        UserCities.Add(newCity);
        SelectedCityAction.SelectedCity = newCity;
    }
}