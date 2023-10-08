using Microsoft.Extensions.DependencyInjection;
using WeatherForecastSystem.CityDriver.Helpers;
using WeatherForecastSystem.Logic.Abstraction;
using WeatherForecastSystem.RedisLogic.Abstraction;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;

var collection = new ServiceCollection();
collection.AddDependencyInjections();
using var provider = collection.BuildServiceProvider();

try
{
    var serviceBusMessagingService = provider.GetService<IServiceBusMessagingService>();
    var redisService = provider.GetService<IRedisService>();
    var cityService = provider.GetService<ICityService>();

    var cityProcessor = new CityProcessor(serviceBusMessagingService!, redisService!, cityService!);
    await cityProcessor.Process();
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
