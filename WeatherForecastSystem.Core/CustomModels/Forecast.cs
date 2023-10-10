namespace WeatherForecastSystem.Core.CustomModels;

public record Forecast(DateTime ForecastDate, float Temperature, float Humidity,
                float WindGust, float Precipitation, float Visibility, float WindSpeed)
{
    
}