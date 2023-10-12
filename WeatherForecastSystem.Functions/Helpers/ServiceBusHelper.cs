using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace WeatherForecastSystem.Functions.Helpers;

public static class ServiceBusHelper
{
    public static ServiceBusClient GetServiceBusClient(string url)
    {
        var clientOptions = new ServiceBusClientOptions {TransportType = ServiceBusTransportType.AmqpWebSockets};
        return new ServiceBusClient(url,clientOptions);
    }

    public static ServiceBusReceiver GetServiceBusReceiver(this ServiceBusClient client,string queue)
    {
        return client.CreateReceiver(queue);
    }
    
    public static ServiceBusSender GetServiceBusSender(this ServiceBusClient client, string queue)
    {
        return client.CreateSender(queue);
    }
}