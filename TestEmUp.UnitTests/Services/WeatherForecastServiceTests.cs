using Source.Models;
using Source.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Source.Tests.Services
{
    public class WeatherForecastServiceTests
    {
        private readonly WeatherForecastService _service;

        public WeatherForecastServiceTests()
        {
            _service = new WeatherForecastService();
        }

        [Fact]
        public void GetForecasts_ReturnsEmptyList_WhenNoForecastsAdded()
        {
            // Act
            var result = _service.GetForecasts();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetForecasts_ReturnsAllAddedForecasts()
        {
            // Arrange
            var forecast1 = new WeatherForecast { Id = 1 };
            var forecast2 = new WeatherForecast { Id = 2 };
            _service.AddForecast(forecast1);
            _service.AddForecast(forecast2);

            // Act
            var result = _service.GetForecasts();

            // Assert
            Assert.Contains(forecast1, result);
            Assert.Contains(forecast2, result);
        }

        [Fact]
        public void GetForecastById_ReturnsCorrectForecast_WhenIdExists()
        {
            // Arrange
            var forecast = new WeatherForecast { Id = 1 };
            _service.AddForecast(forecast);

            // Act
            var result = _service.GetForecastById(1);

            // Assert
            Assert.Equal(forecast, result);
        }

        [Fact]
        public void GetForecastById_ReturnsNull_WhenIdDoesNotExist()
        {
            // Act
            var result = _service.GetForecastById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddForecast_AddsForecastToList()
        {
            // Arrange
            var forecast = new WeatherForecast { Id = 1 };

            // Act
            _service.AddForecast(forecast);
            var result = _service.GetForecasts();

            // Assert
            Assert.Contains(forecast, result);
        }
    }
}
