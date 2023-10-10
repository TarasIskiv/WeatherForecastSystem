using MediatR;
using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.MediatR.Queries;

public class GetCityForecastQuery : IRequest<List<CityForecastClient>>
{
    public City City { get; set; }

    public GetCityForecastQuery(City city)
    {
        City = city;
    }
}