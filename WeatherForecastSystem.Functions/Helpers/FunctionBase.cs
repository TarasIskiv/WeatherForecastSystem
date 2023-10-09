using WeatherForecastSystem.ServiceBusLogic.Abstraction;

namespace WeatherForecastSystem.Functions.Helpers;

public class FunctionBase<T>
{
    protected readonly IServiceBusMessagingService _messagingService;

    
    public FunctionBase(IServiceBusMessagingService messagingService)
    {
        _messagingService = messagingService;
    }
}