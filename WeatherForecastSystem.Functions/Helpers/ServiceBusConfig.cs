namespace WeatherForecastSystem.Functions.Helpers;

public class ServiceBusConfig
{
    public string ServiceBusURL { get; set; }
    public string ServiceBusCityQueue { get; set; }
    public string ServiceBusScrapeQueue { get; set; }
}