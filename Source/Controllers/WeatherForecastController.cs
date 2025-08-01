/// <summary>
/// Dependency on NuGet package 'Microsoft.AspNetCore.Mvc'.
/// </summary>
/// <remarks>
/// This package provides essential components for building web APIs in ASP.NET Core, including controllers, routing, and model binding functionalities.
/// Version: 5.0.0
/// </remarks>
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Dependency on the project 'Source.Models'.
/// </summary>
/// <remarks>
/// This project contains the data model definitions used throughout the application, including the WeatherForecast class.
/// </remarks>
using Source.Models;

/// <summary>
/// Dependency on the project 'Source.Services'.
/// </summary>
/// <remarks>
/// This project encapsulates the business logic and services related to weather forecasting, providing methods for retrieving and manipulating forecast data.
/// </remarks>
using Source.Services;

using System.Diagnostics.CodeAnalysis;

namespace Source.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController" /> class.
        /// </summary>
        /// <param name="weatherForecastService">An instance of the service used to manage weather forecasts.</param>
        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        /// <summary>
        /// Retrieves a list of weather forecasts.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="WeatherForecast" /> objects.</returns>
        /// <remarks>
        /// This action method is invoked via an HTTP GET request.
        /// </remarks>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecastService.GetForecasts();
        }

        /// <summary>
        /// Retrieves a specific weather forecast by ID.
        /// </summary>
        /// <param name="id">The ID of the weather forecast to retrieve.</param>
        /// <returns>An <see cref="ActionResult{WeatherForecast}" /> that may contain the requested forecast or a 404 Not Found if not found.</returns>
        /// <remarks>
        /// This action method is invoked via an HTTP GET request with a specific ID.
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
        /// <param name="forecast">The weather forecast to create.</param>
        /// <returns>An <see cref="ActionResult{WeatherForecast}" /> containing the created forecast and a 201 Created status code.</returns>
        /// <remarks>
        /// This action method is invoked via an HTTP POST request.
        /// </remarks>
        [HttpPost]
        public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
        {
            _weatherForecastService.AddForecast(forecast);
            return CreatedAtAction(nameof(Get), new { id = forecast.Id }, forecast);
        }
    }
}