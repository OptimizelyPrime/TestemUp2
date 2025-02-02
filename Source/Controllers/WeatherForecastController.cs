using Microsoft.AspNetCore.Mvc;
using Source.Models;
using Source.Services;
using System.Diagnostics.CodeAnalysis;

namespace Source.Controllers;

/// <summary>
/// The WeatherForecastController class is responsible for handling HTTP requests related to weather forecasts.
/// </summary>
/// <remarks>
/// Dependencies:
/// - Microsoft.AspNetCore.Mvc (Version: 2.2.0 or later): Provides the core MVC framework for building web APIs.
/// - Source.Models: Contains the WeatherForecast model used for data representation.
/// - Source.Services: Contains the IWeatherForecastService interface for business logic related to weather forecasts.
/// - System.Diagnostics.CodeAnalysis: Provides attributes for code analysis, such as ExcludeFromCodeCoverage.
/// </remarks>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
    /// </summary>
    /// <param name="weatherForecastService">An instance of <see cref="IWeatherForecastService"/> used to retrieve and manage weather forecasts.</param>
    /// <remarks>
    /// Dependencies:
    /// - IWeatherForecastService: Interface for the service that provides weather forecast data.
    /// </remarks>
    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    /// <summary>
    /// Retrieves a collection of weather forecasts.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="WeatherForecast"/> objects.</returns>
    /// <remarks>
    /// Dependencies:
    /// - IWeatherForecastService: Must be implemented to provide the forecast data.
    /// </remarks>
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecastService.GetForecasts();
    }

    /// <summary>
    /// Retrieves a specific weather forecast by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the weather forecast to retrieve.</param>
    /// <returns>An <see cref="ActionResult{WeatherForecast}"/> containing the requested forecast or a 404 Not Found response if not found.</returns>
    /// <exception cref="ArgumentException">Thrown when the id is less than or equal to zero.</exception>
    /// <remarks>
    /// Dependencies:
    /// - IWeatherForecastService: Must be implemented to provide the forecast data.
    /// </remarks>
    [HttpGet("{id}")]
    [ExcludeFromCodeCoverage]
    public ActionResult<WeatherForecast> Get(int id)
    {
        var forecast = _weatherForecastService.GetForecastById(id);
        if (forecast == null)
        {
            return NotFound();
        }
        return forecast;
    }

    /// <summary>
    /// Creates a new weather forecast.
    /// </summary>
    /// <param name="forecast">The <see cref="WeatherForecast"/> object to create.</param>
    /// <returns>An <see cref="ActionResult{WeatherForecast}"/> containing the created forecast.</returns>
    /// <remarks>
    /// Dependencies:
    /// - IWeatherForecastService: Must be implemented to handle the addition of the forecast.
    /// </remarks>
    [HttpPost]
    public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
    {
        _weatherForecastService.AddForecast(forecast);
        return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
    }
}