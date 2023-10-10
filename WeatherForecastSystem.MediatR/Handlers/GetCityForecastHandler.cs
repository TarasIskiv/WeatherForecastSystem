using MediatR;
using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.MediatR.Queries;
using WeatherForecastSystem.RedisLogic.Abstraction;

namespace WeatherForecastSystem.MediatR.Handlers;

public class GetCityForecastHandler : IRequestHandler<GetCityForecastQuery, List<CityForecastClient>>
{
    private readonly IRedisService _redisService;

    public GetCityForecastHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }
    public async Task<List<CityForecastClient>> Handle(GetCityForecastQuery request, CancellationToken cancellationToken)
    {
        var cityName = request.City.CityName;
        var key = _redisService.GetKey(cityName);
        return await _redisService.GetData<List<CityForecastClient>>(key);
    }
}