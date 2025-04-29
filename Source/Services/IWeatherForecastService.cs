using Source.Models;

namespace Source.Services
{
    /// <summary>
    /// Dependency on the external project 'Source.Models'.
    /// </summary>
    /// <remarks>
    /// This dependency is required to access the WeatherForecast class which represents the forecast data. 
    /// It provides the necessary data structure used throughout the IWeatherForecastService interface.
    /// </remarks>
    public interface IWeatherForecastService
    {
        /// <summary>
        /// Retrieves a collection of weather forecasts.
        /// </summary>
        /// <returns>
        /// An enumerable collection of WeatherForecast objects.
        /// </returns>
        /// <remarks>
        /// Requires the Source.Models namespace to access the WeatherForecast class.
        /// </remarks>
        IEnumerable<WeatherForecast> GetForecasts();

        /// <summary>
        /// Retrieves a specific weather forecast by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the weather forecast to retrieve.</param>
        /// <returns>
        /// A WeatherForecast object that corresponds to the specified identifier.
        /// </returns>
        /// <remarks>
        /// Requires the Source.Models namespace to access the WeatherForecast class.
        /// Throws an exception if the forecast with the specified id does not exist.
        /// </remarks>
        WeatherForecast GetForecastById(int id);

        /// <summary>
        /// Adds a new weather forecast to the collection.
        /// </summary>
        /// <param name="forecast">The WeatherForecast object to add.</param>
        /// <returns>
        /// Void.
        /// </returns>
        /// <remarks>
        /// Requires the Source.Models namespace to access the WeatherForecast class.
        /// Throws an exception if the forecast is null or invalid.
        /// </remarks>
        void AddForecast(WeatherForecast forecast);
    }
}