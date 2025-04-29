using NUnit.Framework;
using Source.Models;
using Source.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Source.Tests
{
    [TestFixture]
    public class WeatherForecastServiceTests
    {
        private WeatherForecastService _service;

        [SetUp]
        public void SetUp()
        {
            // This method runs before each test to ensure each test starts with a fresh service instance.
            _service = new WeatherForecastService();
        }

        [Test]
        public void TestAddValidForecastAndRetrieve()
        {
            // Arrange
            var forecast = new WeatherForecast
            {
                Id = 1,
                Date = new DateTime(2023, 11, 1),
                TemperatureC = 25,
                Summary = "Sunny"
            };

            // Act
            _service.AddForecast(forecast);
            var allForecasts = _service.GetForecasts().ToList();

            // Assert
            Assert.AreEqual(1, allForecasts.Count, "Expected exactly one forecast after adding.");
            Assert.AreEqual(1, allForecasts[0].Id, "The ID of the added forecast should be 1.");
            Assert.AreEqual("Sunny", allForecasts[0].Summary, "The Summary of the added forecast should match.");
        }

        [Test]
        public void TestGetForecastByValidId()
        {
            // Arrange
            var forecast = new WeatherForecast
            {
                Id = 2,
                Date = new DateTime(2023, 11, 2),
                TemperatureC = 18,
                Summary = "Cloudy"
            };
            _service.AddForecast(forecast);

            // Act
            var retrievedForecast = _service.GetForecastById(2);

            // Assert
            Assert.IsNotNull(retrievedForecast, "Expected to retrieve a forecast with ID 2.");
            Assert.AreEqual(2, retrievedForecast.Id, "The retrieved forecast should have ID 2.");
            Assert.AreEqual("Cloudy", retrievedForecast.Summary, "The retrieved forecast's Summary should match.");
        }

        [Test]
        public void TestGetForecastByNonExistentId()
        {
            // Act
            var nonExistentForecast = _service.GetForecastById(999);

            // Assert
            Assert.IsNull(nonExistentForecast, "Expected null when retrieving a forecast that doesn't exist.");
        }

        [Test]
        public void TestAddNullForecastThrowsException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _service.AddForecast(null));
            Assert.That(ex!.Message, Does.Contain("Forecast cannot be null."),
                "The exception message should indicate a null forecast is not allowed.");
        }

        [Test]
        public void TestAddMultipleForecastsAndRetrieveAll()
        {
            // Arrange
            var forecast1 = new WeatherForecast
            {
                Id = 10,
                Date = new DateTime(2023, 11, 10),
                TemperatureC = 10,
                Summary = "Rainy"
            };
            var forecast2 = new WeatherForecast
            {
                Id = 11,
                Date = new DateTime(2023, 11, 11),
                TemperatureC = 15,
                Summary = "Windy"
            };

            // Act
            _service.AddForecast(forecast1);
            _service.AddForecast(forecast2);
            var allForecasts = _service.GetForecasts().ToList();

            // Assert
            Assert.AreEqual(2, allForecasts.Count, "Expected two forecasts after adding multiple entries.");
            Assert.IsTrue(allForecasts.Any(f => f.Id == 10), "The first forecast (ID 10) should be present.");
            Assert.IsTrue(allForecasts.Any(f => f.Id == 11), "The second forecast (ID 11) should be present.");
        }
    }
}
