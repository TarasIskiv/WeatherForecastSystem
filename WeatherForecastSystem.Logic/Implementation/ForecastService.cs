using Newtonsoft.Json;
using RestSharp;
using WeatherForecastSystem.Core.CustomModels;
using WeatherForecastSystem.Logic.Abstraction;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WeatherForecastSystem.Logic.Implementation;

public class ForecastService : IForecastService
{
    private readonly string _timeSteps;
    private readonly string _apiKey;
    public ForecastService(string timeSteps, string apiKey)
    {
        _timeSteps = timeSteps;
        _apiKey = apiKey;
    }
    public async Task<List<Forecast>> GetForecast(string cityName)
    {
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        var response = await GetClient(cityName).GetAsync(request);
        if (string.IsNullOrEmpty(response.Content)) return new();
        var forecasts = ParseContent(response.Content);
        return forecasts ?? new();
    }

    private List<Forecast> ParseContent(string input)
    {
        var forecastList = new List<Forecast>();
        dynamic jsonObject = JsonConvert.DeserializeObject(input);

        foreach (var daily in jsonObject?.daily)
        {
            var date = daily?.time ?? DateTime.MinValue;
            var temperatureAvg = daily?.values?.temperatureAvg ?? 0.0f;
            var humidityAvg = daily?.values?.humidityAvg ?? 0.0f;
            var windGustAvg = daily?.values?.windGustAvg ?? 0.0f;
            var precipitationProbabilityAvg = daily?.values?.precipitationProbabilityAvg ?? 0.0f;
            var visibilityAvg = daily?.values?.visibilityAvg ?? 0.0f;
            var windSpeedAvg = daily?.values?.windSpeedAvg ?? 0.0f;

            var forecast = new Forecast(date, temperatureAvg, humidityAvg, windGustAvg, precipitationProbabilityAvg, visibilityAvg, windSpeedAvg);
            forecastList.Add(forecast);
        }

        return forecastList;
    }
    private RestClient GetClient(string location)
    {
        var options = new RestClientOptions($"https://api.tomorrow.io/v4/weather/forecast?location={location}&timesteps={_timeSteps}&apikey={_apiKey}");
        return new RestClient(options);
    }
}