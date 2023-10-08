using System.Text.Json;
using Azure.Messaging.ServiceBus;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;

namespace WeatherForecastSystem.ServiceBusLogic.Implementation;

public class ServiceBusMessagingService : IServiceBusMessagingService, IAsyncDisposable
{
    private readonly ServiceBusSender _sender;
    private readonly ServiceBusReceiver _receiver;

    public ServiceBusMessagingService(ServiceBusSender sender, ServiceBusReceiver receiver)
    {
        _sender = sender;
        _receiver = receiver;
    }
    public async Task SendMessage<T>(T message)
    {
       var messageBatch = await _sender.CreateMessageBatchAsync();
       var serializedMessage = JsonSerializer.Serialize(message);
       if(!messageBatch.TryAddMessage(new (serializedMessage))) return;
       await _sender.SendMessagesAsync(messageBatch);
    }

    public async Task<T> ReceiveMessage<T>()
    {
        var message = await _receiver.ReceiveMessageAsync();
        var deserializedMessage = JsonSerializer.Deserialize<T>(message.Body.ToString());
        return deserializedMessage ?? default!;
    }
    
    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
        await _receiver.DisposeAsync();
    }
}