namespace WeatherForecastSystem.Core.Models;

public record CityForecast(int CityForecastId, int CityId, DateTime ForecastDate, 
                            float Temperature, float Humidity, float WindGust, 
                            float Precipitation, float Visibility, float WindSpeed);