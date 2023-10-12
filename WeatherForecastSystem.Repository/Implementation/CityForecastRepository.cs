using Dapper;
using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.Database;
using WeatherForecastSystem.Repository.Abstraction;

namespace WeatherForecastSystem.Repository.Implementation;

public class CityForecastRepository : ICityForecastRepository
{
    private readonly DapperContext _context;

    public CityForecastRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task RemoveForecastForCities()
    {
        string sql = @"Delete * from CityForecast";
        using var connection = _context.CreateConnection();
        connection.Open();
        await connection.ExecuteAsync(sql);
    }

    public async Task AddForecastForCities(List<CityForecast> forecasts)
    {
        var sql = @"Insert into CityForecast(CityId,ForecastData, Temperature,  Humidity,  WindGust, Precipitation,  Visibility, WindSpeed)
                    Values(@CityId, @ForecastDate, @Temperature,  @Humidity,  @WindGust, @Precipitation,  @Visibility, @WindSpeed)";

        var anonymousForecasts = new List<object>();
        forecasts.ForEach(forecast =>
        {
            anonymousForecasts.Add(new
            {
                CityId = forecast.CityId,
                ForecastDate = forecast.ForecastDate,
                Temperature = forecast.Temperature,
                Humidity = forecast.Humidity,
                WindGust = forecast.WindGust,
                Precipitation = forecast.Precipitation,
                Visibility = forecast.Visibility,
                WindSpeed = forecast.WindSpeed
            });
        });
        using var connection = _context.CreateConnection();
        connection.Open();
        await connection.ExecuteAsync(sql, anonymousForecasts);
    }

    public async Task<List<CityForecast>> GetForecastsForCity(int cityId)
    {
        var sql = @"Select 
                        CityForecastId,
                        CityId,
                        ForecastData,
                        Temperature,  
                        Humidity,  
                        WindGust, 
                        Precipitation,  
                        Visibility,
                        WindSpeed
                    from CityForecast where CityId = @Id";
        using var connection = _context.CreateConnection();
        connection.Open();
        var forecasts = await connection.QueryAsync<CityForecast>(sql, new {Id = cityId});
        return forecasts.ToList();
    }
}