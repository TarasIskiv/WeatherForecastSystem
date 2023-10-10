using MediatR;
using WeatherForecastSystem.Core.Models;
using WeatherForecastSystem.MediatR.Queries;
using WeatherForecastSystem.RedisLogic.Abstraction;

namespace WeatherForecastSystem.MediatR.Handlers;

public class GetCitiesHandler : IRequestHandler<GetCitiesQuery, List<City>>
{
    private readonly IRedisService _redisService;

    public GetCitiesHandler(IRedisService redisService)
    {
        _redisService = redisService;
    }
    public Task<List<City>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var citiesKey = _redisService.GetCityListKey();
        return _redisService.GetData<List<City>>(citiesKey);
    }
}