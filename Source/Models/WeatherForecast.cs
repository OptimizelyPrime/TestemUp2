using System.Diagnostics.CodeAnalysis;

namespace Source.Models
{
    /// <summary>
    /// Represents a weather forecast with details such as date, temperature, and summary.
    /// </summary>
    /// <remarks>
    /// This class is meant to encapsulate weather forecast information and is excluded from code coverage metrics.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets the unique identifier for the weather forecast.
        /// </summary>
        /// <value>The unique identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the weather forecast.
        /// </summary>
        /// <value>The date of the forecast.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in Celsius.
        /// </summary>
        /// <value>The temperature in degrees Celsius.</value>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets or sets the summary of the weather forecast.
        /// </summary>
        /// <value>The summary of the weather conditions.</value>
        public string? Summary { get; set; }
    }
}