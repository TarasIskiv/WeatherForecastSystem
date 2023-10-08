using Microsoft.Extensions.DependencyInjection;
using WeatherForecastSystem.CityDriver.Helpers;

var collection = new ServiceCollection();
collection.AddDependencyInjections();
using var provider = collection.BuildServiceProvider();