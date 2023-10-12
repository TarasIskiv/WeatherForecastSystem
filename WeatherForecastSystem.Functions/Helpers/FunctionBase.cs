using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;
using WeatherForecastSystem.ServiceBusLogic.Implementation;

namespace WeatherForecastSystem.Functions.Helpers;

public class FunctionBase<T>
{
    protected readonly IServiceBusMessagingService _messagingService;
    protected readonly IServiceBusMessagingService _messagingScrapeService;

    public FunctionBase(IConfiguration configuration)
    {
        var config = new ServiceBusConfig();
        configuration.GetSection("ConnectionStrings").Bind(config);
        var client = ServiceBusHelper.GetServiceBusClient(config.ServiceBusURL);
        var receiver = client.GetServiceBusReceiver(config.ServiceBusCityQueue);
        var sender =client.GetServiceBusSender(config.ServiceBusCityQueue);
        _messagingService = new ServiceBusMessagingService(sender, receiver);

        receiver = client.GetServiceBusReceiver(config.ServiceBusScrapeQueue);
        sender =client.GetServiceBusSender(config.ServiceBusScrapeQueue);
        _messagingScrapeService = new ServiceBusMessagingService(sender, receiver);
        
    }
}