using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WeatherForecastSystem.Logic.Abstraction;
using WeatherForecastSystem.RedisLogic.Abstraction;
using WeatherForecastSystem.ScrapeDriver.Helpers;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;

var collection = new ServiceCollection();
collection.AddDependencyInjections();
var provider = collection.BuildServiceProvider();

var redis = provider.GetService<IRedisService>();
var forecastService = provider.GetService<IForecastService>();
var cityForecastService = provider.GetService<ICityForecastService>();
var cityService = provider.GetService<ICityService>();
var serviceBusService = provider.GetService<IServiceBusMessagingService>();

try
{
    var scrapeProcessor = new ScrapeProcessor(redis!, forecastService!, cityForecastService!, cityService!, serviceBusService!);
    await scrapeProcessor.Process();
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
