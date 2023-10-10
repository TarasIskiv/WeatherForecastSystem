using System.Net.Http.Json;
using MediatR;
using WeatherForecastSystem.MediatR.Commands;

namespace WeatherForecastSystem.MediatR.Handlers;

public class CityActionHandler : IRequestHandler<CityActionRequest, bool>
{
    private readonly HttpClient _client;

    public CityActionHandler(HttpClient client)
    {
        _client = client;
    }
    public async Task<bool> Handle(CityActionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"", request.City);
        return response.IsSuccessStatusCode;
    }
}