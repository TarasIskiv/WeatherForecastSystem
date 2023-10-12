using WeatherForecastSystem.Core.CustomModels;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.Core.CustomMapper;

public static class Mapper
{
    public static CityForecast ToCityForecast(this Forecast forecast, int cityId)
    {
        return new CityForecast(0, cityId, forecast.ForecastDate, forecast.Temperature, forecast.Humidity,
            forecast.WindGust, forecast.Precipitation, forecast.Visibility, forecast.WindSpeed);
    }
    
    public static List<CityForecast> ToCityForecastList(this List<Forecast> forecasts, int cityId)
    {
        var cityForecasts = new List<CityForecast>();
        forecasts.ForEach(forecast => cityForecasts.Add(forecast.ToCityForecast(cityId)));
        return cityForecasts;
    }
}