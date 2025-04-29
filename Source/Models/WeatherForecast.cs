using System.Diagnostics.CodeAnalysis;

namespace Source.Models
{
    /// <summary>
    /// Attribute indicating that the code within this class should be excluded from code coverage analysis.
    /// </summary>
    /// <remarks>
    /// This class represents a weather forecast model.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets the identifier for the weather forecast.
        /// </summary>
        /// <value>
        /// The unique identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the weather forecast.
        /// </summary>
        /// <value>
        /// The date when the forecast is applicable.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in Celsius.
        /// </summary>
        /// <value>
        /// The temperature for the forecast in degrees Celsius.</value>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets or sets a summary of the weather forecast.
        /// </summary>
        /// <value>
        /// A short description or summary of the weather forecast.
        /// </value>
        public string? Summary { get; set; }
    }
}