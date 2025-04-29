using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Source.Controllers;
using Source.Models;
using Source.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Controllers
{
    [TestFixture]
    public class WeatherForecastControllerTests
    {
        private Mock<IWeatherForecastService> _mockService;
        private WeatherForecastController _controller;

        [SetUp]
        public void SetUp()
        {
            // Create a mock of the IWeatherForecastService interface
            _mockService = new Mock<IWeatherForecastService>();
            // Initialize the WeatherForecastController with the mocked service
            _controller = new WeatherForecastController(_mockService.Object);
        }

        [Test]
        public void Test_Get_ReturnsListOfForecasts()
        {
            // Arrange: set up mocked data
            var forecasts = new List<WeatherForecast>
            {
                new WeatherForecast { Id = 1, Date = new DateTime(2023, 10, 01), TemperatureC = 20, Summary = "Sunny" },
                new WeatherForecast { Id = 2, Date = new DateTime(2023, 10, 02), TemperatureC = 15, Summary = "Cloudy" }
            };
            _mockService.Setup(s => s.GetForecasts()).Returns(forecasts);

            // Act: call the controller GET method
            var result = _controller.Get();

            // Assert: verify the returned data matches the expected list
            Assert.That(result, Is.Not.Null, "Expected non-null result.");
            Assert.That(result.Count(), Is.EqualTo(2), "Expected exactly two forecasts.");
            Assert.That(result, Is.EquivalentTo(forecasts), "Expected forecasts to match the provided list.");
        }

        [Test]
        public void Test_Get_ReturnsEmptyListWhenNoForecastsAvailable()
        {
            // Arrange: set up empty list
            var forecasts = new List<WeatherForecast>();
            _mockService.Setup(s => s.GetForecasts()).Returns(forecasts);

            // Act
            var result = _controller.Get();

            // Assert: verify the result is empty
            Assert.That(result, Is.Not.Null, "Expected a non-null response.");
            Assert.That(result.Count(), Is.EqualTo(0), "Expected an empty list of forecasts.");
        }

        [Test]
        public void Test_Post_AddsValidForecastAndReturnsCreatedAtAction()
        {
            // Arrange
            var newForecast = new WeatherForecast
            {
                Id = 3,
                Date = new DateTime(2023, 10, 03),
                TemperatureC = 18,
                Summary = "Partly Cloudy"
            };

            // Act: call the POST method
            var actionResult = _controller.Post(newForecast);

            // Assert: verify the correct action result type
            Assert.That(actionResult.Result, Is.TypeOf<CreatedAtActionResult>(), "Expected a CreatedAtActionResult.");
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.That(createdResult, Is.Not.Null, "CreatedAtActionResult should not be null.");
            Assert.That(createdResult.ActionName, Is.EqualTo("Get"), "Expected the action name to be 'Get'.");
            Assert.That(createdResult.RouteValues["id"], Is.EqualTo(newForecast.Id), "Expected the route value 'id' to match the new forecast's Id.");
            Assert.That(createdResult.Value, Is.EqualTo(newForecast), "Expected the CreatedAtActionResult to contain the newly added forecast.");

            // Also verify the service method was called exactly once with the new forecast
            _mockService.Verify(s => s.AddForecast(newForecast), Times.Once);
        }

        [Test]
        public void Test_Post_WithNullForecastThrowsException()
        {
            // Arrange
            WeatherForecast nullForecast = null;

            // Act & Assert: verify that posting a null forecast throws a NullReferenceException
            Assert.Throws<NullReferenceException>(() => _controller.Post(nullForecast),
                "Expected a NullReferenceException when posting a null forecast.");
        }
    }
}
