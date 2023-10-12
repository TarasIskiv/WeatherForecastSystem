using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecastSystem.Database;
using WeatherForecastSystem.Logic.Abstraction;
using WeatherForecastSystem.Logic.Implementation;
using WeatherForecastSystem.RedisLogic.Abstraction;
using WeatherForecastSystem.RedisLogic.Implementation;
using WeatherForecastSystem.Repository.Abstraction;
using WeatherForecastSystem.Repository.Implementation;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;
using WeatherForecastSystem.ServiceBusLogic.Implementation;

namespace WeatherForecastSystem.ScrapeDriver.Helpers;

public static class ScrapeDriverHepler
{
    public static void AddDependencyInjections(this ServiceCollection collection)
    {
        var config = GetConfiguration();
        collection.AddRedis(config);
        var client = GetServiceBusClient(config);
        var receiver = client.GetServiceBusReceiver(config);
        var sender = client.GetServiceBusSender(config);
        var timesteps = config.GetConnectionString("timesteps");
        var apikey = config.GetConnectionString("apikey");
        collection
            .AddSingleton<IConfiguration>(config)
            .AddSingleton<DapperContext>()
            .AddTransient<IServiceBusMessagingService>(service => new ServiceBusMessagingService(sender, receiver))
            .AddScoped<IRedisService, RedisService>()
            .AddScoped<IForecastService>(opt => new ForecastService(timesteps!, apikey!))
            .AddScoped<ICityService, CityService>()
            .AddScoped<ICityRepository, CityRepository>()
            .AddScoped<ICityForecastService, CityForecastService>()
            .AddScoped<ICityForecastRepository, CityForecastRepository>();
    }

    private static ServiceBusClient GetServiceBusClient(IConfiguration configuration)
    {
        var clientOptions = new ServiceBusClientOptions {TransportType = ServiceBusTransportType.AmqpWebSockets};
        var a = configuration.GetConnectionString("ServiceBusURL");
        return new ServiceBusClient(configuration.GetConnectionString("ServiceBusURL"),clientOptions);
    }

    private static ServiceBusReceiver GetServiceBusReceiver(this ServiceBusClient client, IConfiguration configuration)
    {
        return client.CreateReceiver(configuration.GetConnectionString("ServiceBusQueueName"));
    }
    
    private static ServiceBusSender GetServiceBusSender(this ServiceBusClient client, IConfiguration configuration)
    {
        return client.CreateSender(configuration.GetConnectionString("ServiceBusQueueName"));
    }
    
    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.InitializeBuilder();
        return builder.Build();
    }

    private static void AddRedis(this ServiceCollection collection,IConfiguration config)
    {
        collection.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = config.GetConnectionString("Redis");
            opt.InstanceName = "WeatherForecastSystem/";
        });
    }

    private static void InitializeBuilder(this ConfigurationBuilder builder)
    {
        builder.AddJsonFile("appsettings.json", optional: false);
    }
}