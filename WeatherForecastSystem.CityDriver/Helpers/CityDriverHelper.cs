using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecastSystem.Database;
using WeatherForecastSystem.Repository.Abstraction;
using WeatherForecastSystem.Repository.Implementation;

namespace WeatherForecastSystem.CityDriver.Helpers;

public static class CityDriverHelper
{
    public static void AddDependencyInjections(this ServiceCollection collection)
    {
        var config = GetConfiguration();
        collection
            .AddSingleton<DapperContext>()
            .AddScoped<ICityRepository, CityRepository>();
    }
    
    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.InitializeBuilder();
        return builder.Build();
    }

    private static void InitializeBuilder(this ConfigurationBuilder builder)
    {
        builder.AddJsonFile("appsettings.json", optional: false);
    }
}