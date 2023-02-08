using Microsoft.AspNetCore.Mvc;
using Study.CleanArchitecture.Application.Services.WeatherForecasts.Queries.GetWeatherForecasts;

namespace Study.CleanArchitecture.WebAPI.Controllers;

public class WeatherForecastController : ApiControllerBase
{


    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var request = new GetWeatherForecastsQuery();
        return await Mediator.Send(request);
    }
}