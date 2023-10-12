using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherForecastSystem.Functions.Helpers;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;

namespace WeatherForecastSystem.Functions;

public class ScrapeForecast : FunctionBase<ScrapeForecast>
{
    public ScrapeForecast(IConfiguration configuration) : base(configuration)
    {
    }
    [Function("ScrapeForecast")]
    public async Task Run([TimerTrigger("0 3 * * * *", RunOnStartup = true)] TimerInfo myTimer, FunctionContext context)
    {
        await _messagingScrapeService.SendMessage("Scrape");
    }
}
