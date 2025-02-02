using Source.Models;

namespace Source.Services;

/// <summary>
/// Represents a service for managing weather forecasts.
/// </summary>
/// <remarks>
/// Dependencies:
/// - Source.Models: This namespace contains the WeatherForecast model used in this service.
/// - No external libraries or NuGet packages are required for this interface.
/// </remarks>
public interface IWeatherForecastService
{
    /// <summary>
    /// Retrieves a collection of weather forecasts.
    /// </summary>
    /// <returns>
    /// An enumerable collection of <see cref="WeatherForecast"/> objects.
    /// </returns>
    IEnumerable<WeatherForecast> GetForecasts();

    /// <summary>
    /// Retrieves a specific weather forecast by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the weather forecast to retrieve.</param>
    /// <returns>
    /// The <see cref="WeatherForecast"/> object associated with the specified identifier.
    /// </returns>
    /// <exception cref="KeyNotFoundException">Thrown when no forecast is found with the specified id.</exception>
    WeatherForecast GetForecastById(int id);

    /// <summary>
    /// Adds a new weather forecast to the collection.
    /// </summary>
    /// <param name="forecast">The <see cref="WeatherForecast"/> object to add.</param>
    /// <remarks>
    /// This method does not return a value.
    /// </remarks>
    void AddForecast(WeatherForecast forecast);
}