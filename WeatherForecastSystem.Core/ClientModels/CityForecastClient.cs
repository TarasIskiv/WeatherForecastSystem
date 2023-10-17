namespace WeatherForecastSystem.Core.ClientModels;

public record CityForecastClient (int CityForecastId, int CityId, string CityName, DateTime ForecastDate, 
                                    float Temperature, float Humidity, float WindGust, 
                                    float Precipitation, float Visibility, float WindSpeed)
{
    public CityForecastClient() : this(0, 0, default!, default!,default!, default!,default!, default!,default!, default!)
    {}
}