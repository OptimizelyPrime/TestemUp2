using Microsoft.AspNetCore.Mvc;
using Moq;
using Source.Controllers;
using Source.Models;
using Source.Services;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;

namespace Source.Tests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<IWeatherForecastService> _mockService;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            _mockService = new Mock<IWeatherForecastService>();
            _controller = new WeatherForecastController(_mockService.Object);
        }

        [Fact]
        public void Get_ReturnsForecasts()
        {
            // Arrange
            var forecasts = new List<WeatherForecast> { new WeatherForecast() };
            _mockService.Setup(service => service.GetForecasts()).Returns(forecasts);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.Equal(forecasts, result);
        }

        [Fact]
        public void Get_WithValidId_ReturnsForecast()
        {
            // Arrange
            var forecast = new WeatherForecast { Id = 1 };
            _mockService.Setup(service => service.GetForecastById(1)).Returns(forecast);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<ActionResult<WeatherForecast>>(result);
            Assert.Equal(forecast, result.Value);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetForecastById(1)).Returns((WeatherForecast)null);

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Post_AddsForecast_ReturnsCreatedAtAction()
        {
            // Arrange
            var forecast = new WeatherForecast { Id = 1 };
            _mockService.Setup(service => service.AddForecast(forecast));

            // Act
            var result = _controller.Post(forecast);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.Get), createdAtActionResult.ActionName);
            Assert.Equal(forecast, createdAtActionResult.Value);
        }
    }
}
