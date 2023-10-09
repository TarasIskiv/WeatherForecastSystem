using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace WeatherForecastSystem.Functions.Helpers;

public static class ServiceBusHelper
{
    public static ServiceBusClient GetServiceBusClient(IConfiguration configuration)
    {
        var clientOptions = new ServiceBusClientOptions {TransportType = ServiceBusTransportType.AmqpWebSockets};
        return new ServiceBusClient(configuration.GetConnectionString("ServiceBusURL"),clientOptions);
    }

    public static ServiceBusReceiver GetServiceBusReceiver(this ServiceBusClient client, IConfiguration configuration)
    {
        return client.CreateReceiver(configuration.GetConnectionString("ServiceBusCityQueue"));
    }
    
    public static ServiceBusSender GetServiceBusSender(this ServiceBusClient client, IConfiguration configuration)
    {
        return client.CreateSender(configuration.GetConnectionString("ServiceBusCityQueue"));
    }
}