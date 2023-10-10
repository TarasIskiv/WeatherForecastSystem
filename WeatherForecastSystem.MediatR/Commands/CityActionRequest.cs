using MediatR;
using WeatherForecastSystem.Core.ClientModels;

namespace WeatherForecastSystem.MediatR.Commands;

public class CityActionRequest : IRequest<bool>
{
    public CityAction City { get; set; }

    public CityActionRequest(CityAction city)
    {
        City = city;
    }
}