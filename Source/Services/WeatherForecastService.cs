using Source.Models;

namespace Source.Services
{
    /// <summary>
    /// Service class for managing weather forecasts.
    /// </summary>
    /// <remarks>
    /// Dependency on project reference 'Source.Models'.
    /// This dependency is required because the WeatherForecastService class uses the WeatherForecast model for managing weather forecast data.
    /// The WeatherForecast type is essential for the methods provided in this service class, including GetForecasts, GetForecastById, and AddForecast.
    /// </remarks>
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly List<WeatherForecast> _forecasts = new();

        /// <summary>
        /// Retrieves all weather forecasts.
        /// </summary>
        /// <returns>
        /// An enumerable collection of WeatherForecast objects.
        /// </returns>
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            return _forecasts;
        }

        /// <summary>
        /// Retrieves a specific weather forecast by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the weather forecast.</param>
        /// <returns>
        /// A WeatherForecast object if found; otherwise, null.
        /// </returns>
        public WeatherForecast GetForecastById(int id)
        {
            return _forecasts.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Adds a new weather forecast to the collection.
        /// </summary>
        /// <param name="forecast">The WeatherForecast object to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided forecast is null.</exception>
        public void AddForecast(WeatherForecast forecast)
        {
            if (forecast == null)
            {
                throw new ArgumentNullException(nameof(forecast), "Forecast cannot be null.");
            }
            _forecasts.Add(forecast);
        }
    }
}