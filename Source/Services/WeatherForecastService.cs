using Source.Models;

namespace Source.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly List<WeatherForecast> _forecasts = new();

    /// <summary>
    /// Retrieves all weather forecasts.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="WeatherForecast"/> objects.</returns>
    /// <remarks>This function requires the <see cref="Source.Models"/> namespace to access the <see cref="WeatherForecast"/> class.</remarks>
    public IEnumerable<WeatherForecast> GetForecasts()
    {
        return _forecasts;
    }

    /// <summary>
    /// Retrieves a specific weather forecast by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the weather forecast to retrieve.</param>
    /// <returns>A <see cref="WeatherForecast"/> object if found; otherwise, null.</returns>
    /// <remarks>This function requires the <see cref="Source.Models"/> namespace to access the <see cref="WeatherForecast"/> class.</remarks>
    public WeatherForecast GetForecastById(int id)
    {
        return _forecasts.FirstOrDefault(f => f.Id == id);
    }

    /// <summary>
    /// Adds a new weather forecast to the collection.
    /// </summary>
    /// <param name="forecast">The <see cref="WeatherForecast"/> object to add to the collection.</param>
    /// <remarks>This function requires the <see cref="Source.Models"/> namespace to access the <see cref="WeatherForecast"/> class.</remarks>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="forecast"/> is null.</exception>
    public void AddForecast(WeatherForecast forecast)
    {
        if (forecast == null)
        {
            throw new ArgumentNullException(nameof(forecast), "Forecast cannot be null.");
        }
        _forecasts.Add(forecast);
    }
}