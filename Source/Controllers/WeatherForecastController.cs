using Microsoft.AspNetCore.Mvc;
using Source.Models;
using Source.Services;
using System.Diagnostics.CodeAnalysis;

namespace Source.Controllers;

/// <summary>
/// Dependency on the NuGet package 'Microsoft.AspNetCore.Mvc'.
/// </summary>
/// <remarks>
/// This package is essential for building web APIs using ASP.NET Core. It provides the foundational controller classes,
/// action results, routing, and model binding features that are necessary for the functionality of this controller.
/// The version used is 2.2.0 or higher depending on the ASP.NET Core version installed in the project.
/// </remarks>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    /// <summary>
    /// Dependency on the project interface 'IWeatherForecastService'.
    /// </summary>
    /// <remarks>
    /// This interface is crucial for abstraction in the application, allowing the controller to interact with the weather forecast service.
    /// It provides methods for retrieving and manipulating weather forecast data, ensuring that the controller logic remains clean and maintainable.
    /// </remarks>
    private readonly IWeatherForecastService _weatherForecastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
    /// </summary>
    /// <param name="weatherForecastService">An instance of the weather forecast service implementing <see cref="IWeatherForecastService"/>.</param>
    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    /// <summary>
    /// Gets the weather forecasts.
    /// </summary>
    /// <returns>An enumeration of <see cref="WeatherForecast"/> objects.</returns>
    /// <remarks>
    /// This method retrieves all weather forecasts from the underlying service and returns them.
    /// It does not take any parameters and will call the service method to fetch data.
    /// </remarks>
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecastService.GetForecasts();
    }

    /// <summary>
    /// Gets a specific weather forecast by ID.
    /// </summary>
    /// <param name="id">The ID of the weather forecast.</param>
    /// <returns>An <see cref="ActionResult{WeatherForecast}"/> containing the weather forecast or a 404 Not Found result.</returns>
    /// <remarks>
    /// This method retrieves a weather forecast by its unique identifier.
    /// If the forecast is not found, it returns a 404 Not Found response.
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
    /// Adds a new weather forecast.
    /// </summary>
    /// <param name="forecast">The weather forecast to add.</param>
    /// <returns>An <see cref="ActionResult{WeatherForecast}"/> containing the created weather forecast.</returns>
    /// <remarks>
    /// This method creates a new weather forecast by sending it to the underlying service.
    /// The created forecast will be returned with a 201 Created response.
    /// </remarks>
    [HttpPost]
    public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
    {
        _weatherForecastService.AddForecast(forecast);
        return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
    }
}