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
        dynamic data = JsonConvert.DeserializeObject(input);

        if (data.timelines != null && data.timelines.daily != null)
        {
            foreach (var daily in data.timelines.daily)
            {
                DateTime date = DateTime.Parse(daily.time.ToString());
                float temperatureAvg = (float)daily.values.temperatureAvg;
                float humidityAvg = (float)daily.values.humidityAvg;
                float windGustAvg = (float)daily.values.windGustAvg;
                float precipitationProbabilityAvg = (float)daily.values.precipitationProbabilityAvg;
                float visibilityAvg = (float)daily.values.visibilityAvg;
                float windSpeedAvg = (float)daily.values.windSpeedAvg;

                forecastList.Add(new Forecast(date, temperatureAvg, humidityAvg, windGustAvg, precipitationProbabilityAvg, visibilityAvg, windSpeedAvg));
            }
        }

        return forecastList;
    }
    private RestClient GetClient(string location)
    {
        var options = new RestClientOptions($"https://api.tomorrow.io/v4/weather/forecast?location={location}&timesteps={_timeSteps}&apikey={_apiKey}");
        return new RestClient(options);
    }
}