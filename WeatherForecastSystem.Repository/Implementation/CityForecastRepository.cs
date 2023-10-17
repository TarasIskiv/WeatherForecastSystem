using Dapper;
using WeatherForecastSystem.Core.ClientModels;
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
    public async Task RemoveForecastForCity(int cityId)
    {
        string sql = @"delete from CityForecast where CityId = @Id";
        using var connection = _context.CreateConnection();
        connection.Open();
        await connection.ExecuteAsync(sql, new {Id = cityId});
    }

    public async Task AddForecastForCities(List<CityForecast> forecasts)
    {
        var sql = @"Insert into CityForecast(CityId,ForecastDate, Temperature,  Humidity,  WindGust, Precipitation,  Visibility, WindSpeed)
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

    public async Task<List<CityForecastClient>> GetForecastsForCity(int cityId)
    {
        var sql = @"Select
                        CityForecastId,
                        CityId,
                        CityName,
                        ForecastDate,
                        Round(Temperature, 2) Temperature,
                        Round(Humidity, 2) Humidity,
                        Round(WindGust, 2) WindGust,
                        Round(Precipitation, 2) Precipitation,
                        Round(Visibility, 2) Visibility,
                        Round(WindSpeed, 2) WindSpeed
                    from vwCityForecast where CityId = @Id";
        using var connection = _context.CreateConnection();
        connection.Open();
        var forecasts = await connection.QueryAsync<CityForecastClient>(sql, new {Id = cityId});
        return forecasts.ToList();
    }
}