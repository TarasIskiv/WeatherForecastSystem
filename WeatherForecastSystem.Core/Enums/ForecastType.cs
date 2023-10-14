using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecastSystem.Core.Enums;

public enum ForecastType
{ 
    Temperature = 1,
    Humidity,
    WindGust,
    Precipitation,
    Visibility,
    WindSpeed
}