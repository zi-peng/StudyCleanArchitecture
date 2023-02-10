using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Study.CleanArchitecture.Application.Services.TodoLists.Commands.CreateTodoList;
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
    public async Task<IEnumerable<WeatherForecast>?> Get()
    {
        var request = new CreateTodoListCommand();
        
        await Mediator.Send(request);
        return default;
    }
}