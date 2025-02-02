using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

[TestFixture]
public class WeatherForecastServiceTests
{
    private IWeatherForecastService _service;

    [SetUp]
    public void Setup()
    {
        // Initialize the service before each test
        _service = new WeatherForecastService();
    }

    [Test]
    public void test_retrieve_all_forecasts()
    {
        // Act
        var result = _service.GetAllForecasts();

        // Assert
        Assert.IsInstanceOf<IEnumerable<WeatherForecast>>(result);
        Assert.IsTrue(result.Any()); // Assuming there are some forecasts to begin with
    }

    [Test]
    public void test_retrieve_forecast_by_valid_id()
    {
        // Arrange
        var validId = 1;

        // Act
        var result = _service.GetForecastById(validId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(validId, result.Id);
    }

    [Test]
    public void test_retrieve_forecast_by_invalid_id()
    {
        // Arrange
        var invalidId = 999;

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _service.GetForecastById(invalidId));
    }

    [Test]
    public void test_add_valid_forecast()
    {
        // Arrange
        var forecast = new WeatherForecast { Id = 1, TemperatureC = 25, Summary = "Sunny" };

        // Act
        _service.AddForecast(forecast);

        // Assert
        var result = _service.GetForecastById(forecast.Id);
        Assert.AreEqual(forecast, result);
    }

    [Test]
    public void test_add_null_forecast()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.AddForecast(null));
    }

    [Test]
    public void test_retrieve_forecast_by_id_with_no_forecasts_present()
    {
        // Arrange
        var id = 1;

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _service.GetForecastById(id));
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up resources after each test if necessary
    }
}