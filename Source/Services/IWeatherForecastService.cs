using Source.Models;

namespace Source.Services
{
    /// <summary>
    /// Dependency on external project 'Source.Models'.
    /// </summary>
    /// <remarks>
    /// This project contains the definition of the 'WeatherForecast' class used within this service interface. It provides the structure that describes the forecast data utilized by the service.
    /// </remarks>
    public interface IWeatherForecastService
    {
        /// <summary>
        /// Retrieves all weather forecasts.
        /// </summary>
        /// <returns>Enumerable collection of <see cref="WeatherForecast"/>.</returns>
        /// <remarks>
        /// This method requires the 'System.Collections.Generic' namespace for the IEnumerable interface.
        /// </remarks>
        IEnumerable<WeatherForecast> GetForecasts();

        /// <summary>
        /// Retrieves a specific weather forecast by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the weather forecast.</param>
        /// <returns>A <see cref="WeatherForecast"/> object.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the forecast with the specified identifier is not found.</exception>
        /// <remarks>
        /// This method assumes that the forecast must exist in the collection; otherwise, an exception is thrown.
        /// </remarks>
        WeatherForecast GetForecastById(int id);

        /// <summary>
        /// Adds a new weather forecast to the collection.
        /// </summary>
        /// <param name="forecast">The weather forecast to add.</param>
        /// <returns>Void.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided forecast is null.</exception>
        /// <remarks>
        /// This method requires validation to ensure that the forecast is not null before adding it to the collection.
        /// </remarks>
        void AddForecast(WeatherForecast forecast);
    }
}