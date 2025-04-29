using Microsoft.AspNetCore.Mvc;
using Source.Models;
using Source.Services;
using System.Diagnostics.CodeAnalysis;

namespace Source.Controllers;

/// <summary>
/// Dependency on NuGet package 'Microsoft.AspNetCore.Mvc'.
/// </summary>
/// <remarks>
/// This package provides the core functionality for creating MVC applications in ASP.NET Core, including controller actions and model binding. It is essential for the WeatherForecastController to handle HTTP requests and responses properly.
/// Version: 2.2.0 or later required for compatibility with ASP.NET Core.
/// </remarks>
[ApiController]
[Route("[controller]")]  
public class WeatherForecastController : ControllerBase
{
    /// <summary>
    /// Dependency on external service 'IWeatherForecastService'.
    /// </summary>
    /// <remarks>
    /// This is a custom service interface that encapsulates business logic related to weather forecasts. It provides methods to retrieve and manipulate weather forecast data, needed for the operations in this controller.
    /// No version as it is part of the local project.
    /// </remarks>
    private readonly IWeatherForecastService _weatherForecastService;

    /// <summary>
    /// Initializes a new instance of the WeatherForecastController.
    /// </summary>
    /// <param name="weatherForecastService">An instance of IWeatherForecastService used to get forecasts.</param>
    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    /// <summary>
    /// Retrieves all weather forecasts.
    /// </summary>
    /// <returns>An IEnumerable of WeatherForecast objects.</returns>
    /// <remarks>
    /// This method depends on the IWeatherForecastService to fetch the forecasts.
    /// </remarks>
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecastService.GetForecasts();
    }

    /// <summary>
    /// Retrieves a specific weather forecast by its ID.
    /// </summary>
    /// <param name="id">The ID of the desired WeatherForecast.</param>
    /// <returns>An ActionResult containing the WeatherForecast object or a NotFound result.</returns>
    /// <exception cref="NotFoundException">Thrown when a forecast with the specified ID is not found.</exception>
    /// <remarks>
    /// This method uses the IWeatherForecastService to fetch a specific forecast from storage.
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
    /// <param name="forecast">The WeatherForecast object to add.</param>
    /// <returns>An ActionResult containing the newly created WeatherForecast.</returns>
    /// <remarks>
    /// This method adds a new forecast by invoking the corresponding method in the IWeatherForecastService.
    /// </remarks>
    [HttpPost]
    public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
    {
        _weatherForecastService.AddForecast(forecast);
        return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
    }
}