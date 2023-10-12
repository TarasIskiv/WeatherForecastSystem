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
        var config = new ServiceBusConfig();
        context.Configuration.GetSection("ConnectionStrings").Bind(config);
        services
            .AddLogging();
    })
    .UseDefaultServiceProvider(options => options.ValidateScopes = false)
    .Build();

host.Run();
