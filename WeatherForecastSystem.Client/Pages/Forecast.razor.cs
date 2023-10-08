using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace WeatherForecastSystem.Client.Pages;

partial class Forecast
{
    [Parameter, AllowNull] public string City { get; set; } = default!;
}