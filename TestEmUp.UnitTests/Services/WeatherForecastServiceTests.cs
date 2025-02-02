using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class WeatherForecastServiceTests
{
    private WeatherForecastService _service;

    [SetUp]
    public void Setup()
    {
        _service = new WeatherForecastService();
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up resources if needed
    }

    [Test]
    public void test_retrieve_all_forecasts_no_forecasts_added()
    {
        // Arrange
        // No forecasts added yet

        // Act
        var result = _service.GetForecasts();

        // Assert
        Assert.IsEmpty(result);
    }

    [Test]
    public void test_retrieve_all_forecasts_one_forecast_added()
    {
        // Arrange
        var forecast = new WeatherForecast { Id = 1, TemperatureC = 20, Summary = "Sunny" };
        _service.AddForecast(forecast);

        // Act
        var result = _service.GetForecasts();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(forecast, result[0]);
    }

    [Test]
    public void test_retrieve_forecast_by_valid_id()
    {
        // Arrange
        var forecast = new WeatherForecast { Id = 1, TemperatureC = 20, Summary = "Sunny" };
        _service.AddForecast(forecast);

        // Act
        var result = _service.GetForecastById(1);

        // Assert
        Assert.AreEqual(forecast, result);
    }

    [Test]
    public void test_retrieve_forecast_by_invalid_id()
    {
        // Arrange
        // No forecasts added yet

        // Act
        var result = _service.GetForecastById(999);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void test_add_valid_forecast()
    {
        // Arrange
        var forecast = new WeatherForecast { Id = 1, TemperatureC = 20, Summary = "Sunny" };

        // Act
        _service.AddForecast(forecast);

        // Assert
        var result = _service.GetForecastById(1);
        Assert.AreEqual(forecast, result);
    }

    [Test]
    public void test_add_null_forecast()
    {
        // Arrange
        WeatherForecast forecast = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.AddForecast(forecast));
    }
}