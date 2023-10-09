using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecastSystem.Functions.Helpers;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;
using WeatherForecastSystem.ServiceBusLogic.Implementation;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddJsonFile("appsettings.json", optional: false);
    })
    .ConfigureServices((context, services) =>
    {
        var client = ServiceBusHelper.GetServiceBusClient(context.Configuration);
        var sender =  client.GetServiceBusSender(context.Configuration);
        var receiver = client.GetServiceBusReceiver(context.Configuration);
        services
            .AddLogging()
            .AddScoped<IServiceBusMessagingService>(opt => new ServiceBusMessagingService(sender, receiver));
    })
    .UseDefaultServiceProvider(options => options.ValidateScopes = false)
    .Build();

host.Run();
