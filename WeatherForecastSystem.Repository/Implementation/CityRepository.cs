using Dapper;
using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.Database;
using WeatherForecastSystem.Repository.Abstraction;

namespace WeatherForecastSystem.Repository.Implementation;

public class CityRepository : ICityRepository
{
    private readonly DapperContext _context;

    public CityRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task CreateCity(string cityName)
    {
        var sql = @"Insert into Cities(CityName, IsInUse) Values (@Name, @IsInUse)";
        using var connection = _context.CreateConnection();
        connection.Open();
        await connection.ExecuteAsync(sql, new { Name = cityName, IsInUse = 1 });
    }

    public async Task RemoveCity(int id)
    {
        var sql = @"Delete from Cities where CityId = @Id";
        using var connection = _context.CreateConnection();
        connection.Open();
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task UpdateCity(int id, string cityName)
    {
        var sql = @"Update Cities
                    Set CityName = @Name
                    Where CityId = @Id";
        using var connection = _context.CreateConnection();
        connection.Open();
        await connection.ExecuteAsync(sql, new { Name = cityName, Id = id });
    }

    public async Task<bool> VerifyIfCityExists(string cityName)
    {
        var sql = @"select count(*) from Cities
                    where CityName = @Name";
        using var connection = _context.CreateConnection();
        connection.Open();
        var count = await connection.QuerySingleAsync<int>(sql, new { Name = cityName});
        return count != 0;
    }

    public async Task<List<City>> GetCities()
    {
        var sql = @"Select CityId, CityName, IsInUse from Citites";
        using var connection = _context.CreateConnection();
        connection.Open();
        var cities = await connection.QueryAsync<City>(sql);
        return cities.ToList();
    }
}