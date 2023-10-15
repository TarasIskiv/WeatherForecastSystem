using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.MediatR.Commands;

namespace WeatherForecastSystem.MediatR.Handlers;

public class CityActionHandler : IRequestHandler<CityActionRequest, bool>
{
    private readonly HttpClient _client;

    public CityActionHandler()
    {
        _client = new HttpClient();
    }
    public async Task<bool> Handle(CityActionRequest request, CancellationToken cancellationToken)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7071/api/ProcessCityAction");
        httpRequest.Content = new StringContent(JsonSerializer.Serialize(request.City), Encoding.UTF8, "application/json");
        var response =await _client.SendAsync(httpRequest);
        return response.IsSuccessStatusCode;
    }
}