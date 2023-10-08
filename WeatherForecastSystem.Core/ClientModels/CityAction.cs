using WeatherForecastSystem.Core.Enums;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Core.ClientModels;

public class CityAction
{
    public City SelectedCity { get; set; } = default!;
    public ActionType Action { get; set; } = ActionType.Create;
}