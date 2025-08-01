using Source.Models;

namespace Source.Services
{
    /// <summary>
    /// Service for managing weather forecasts.
    /// </summary>
    /// <remarks>
    /// Dependency on project reference 'Source.Models'.
    /// The WeatherForecast class is defined in the Source.Models namespace, and it represents the data structure used for storing weather forecast information.
    /// This dependency is essential as WeatherForecastService manages collections of WeatherForecast objects.
    /// </remarks>
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly List<WeatherForecast> _forecasts = new(); 

        /// <summary>
        /// Retrieves all weather forecasts.
        /// </summary>
        /// <returns>An enumerable collection of WeatherForecast objects.</returns>
        /// <remarks>No external dependencies required.</remarks>
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            return _forecasts;
        }

        /// <summary>
        /// Retrieves a specific weather forecast by its ID.
        /// </summary>
        /// <param name="id">The ID of the weather forecast to retrieve, of type <see cref="int"/>.</param>
        /// <returns>The corresponding WeatherForecast object if found; otherwise, null.</returns>
        /// <remarks>No external dependencies required.</remarks>
        public WeatherForecast GetForecastById(int id)
        {
            return _forecasts.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Adds a new weather forecast to the collection.
        /// </summary>
        /// <param name="forecast">The WeatherForecast object to add, of type <see cref="WeatherForecast"/>.</param>
        /// <remarks>No external dependencies required.</remarks>
        /// <exception cref="ArgumentNullException">Thrown when the forecast parameter is null.</exception>
        public void AddForecast(WeatherForecast forecast)
        {
            if (forecast is null) throw new ArgumentNullException(nameof(forecast), "Forecast cannot be null.");
            _forecasts.Add(forecast);
        }
    }
}