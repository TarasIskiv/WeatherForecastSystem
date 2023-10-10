using MediatR;
using WeatherForecastSystem.Core.Models;

namespace WeatherForecastSystem.MediatR.Queries;

public class GetCitiesQuery : IRequest<List<City>>
{
    
}