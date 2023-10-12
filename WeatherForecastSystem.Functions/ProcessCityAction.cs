using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeatherForecastSystem.Core.ClientModels;
using WeatherForecastSystem.Functions.Helpers;
using WeatherForecastSystem.ServiceBusLogic.Abstraction;

namespace WeatherForecastSystem.Functions;

public class ProcessCityAction : FunctionBase<ProcessCityAction>
{
    public ProcessCityAction(IConfiguration configuration) : base(configuration)
    {
        
    }
    [Function("ProcessCityAction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        try
        {
            using StreamReader reader = new StreamReader(req.Body, Encoding.UTF8);
            var data = reader.ReadToEnd();
            var cityAction = JsonSerializer.Deserialize<CityAction>(data);
            await _messagingService.SendMessage(cityAction);
            return req.CreateResponse(HttpStatusCode.Created);
        }
        catch (Exception e)
        {
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}
