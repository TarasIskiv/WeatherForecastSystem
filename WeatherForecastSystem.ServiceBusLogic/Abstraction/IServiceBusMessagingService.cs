namespace WeatherForecastSystem.ServiceBusLogic.Abstraction;

public interface IServiceBusMessagingService
{
    Task SendMessage<T>(T message);
    Task<T> ReceiveMessage<T>();
}