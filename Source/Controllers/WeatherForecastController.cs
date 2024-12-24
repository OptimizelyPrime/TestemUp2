using Microsoft.AspNetCore.Mvc;
using Source.Services;

namespace Source.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecastService.GetForecasts();
    }

    [HttpGet("{id}")]
    public ActionResult<WeatherForecast> Get(int id)
    {
        var forecast = _weatherForecastService.GetForecastById(id);
        if (forecast == null)
        {
            return NotFound();
        }
        return forecast;
    }

    [HttpPost]
    public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
    {
        _weatherForecastService.AddForecast(forecast);
        return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
    }
}
