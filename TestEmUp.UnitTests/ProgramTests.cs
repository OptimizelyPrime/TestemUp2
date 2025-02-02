using NUnit.Framework;
using System;

[TestFixture]
public class WeatherForecastServiceTests
{
    private WeatherForecastService _service;

    [SetUp]
    public void Setup()
    {
        _service = new WeatherForecastService();
    }

    [Test]
    public void test_valid_weather_forecast_request()
    {
        var result = _service.GetWeatherForecast("New York", "2023-10-01");
        Assert.IsNotNull(result);
        Assert.AreEqual("New York", result.Location);
        Assert.AreEqual("2023-10-01", result.Date);
    }

    [Test]
    public void test_edge_case_boundary_date()
    {
        var result = _service.GetWeatherForecast("Los Angeles", "2023-01-01");
        Assert.IsNotNull(result);
        Assert.AreEqual("Los Angeles", result.Location);
        Assert.AreEqual("2023-01-01", result.Date);
    }

    [Test]
    public void test_invalid_location()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.GetWeatherForecast("InvalidLocation", "2023-10-01"));
        Assert.AreEqual("Invalid location specified.", ex.Message);
    }

    [Test]
    public void test_null_location()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.GetWeatherForecast("", "2023-10-01"));
        Assert.AreEqual("Location cannot be null or empty.", ex.Message);
    }

    [Test]
    public void test_invalid_date_format()
    {
        var ex = Assert.Throws<FormatException>(() => _service.GetWeatherForecast("Chicago", "invalid-date"));
        Assert.AreEqual("Invalid date format.", ex.Message);
    }

    [Test]
    public void test_valid_weather_forecast_future_date()
    {
        var result = _service.GetWeatherForecast("Miami", "2023-12-25");
        Assert.IsNotNull(result);
        Assert.AreEqual("Miami", result.Location);
        Assert.AreEqual("2023-12-25", result.Date);
    }

    [Test]
    public void test_leap_year_date()
    {
        var result = _service.GetWeatherForecast("Seattle", "2024-02-29");
        Assert.IsNotNull(result);
        Assert.AreEqual("Seattle", result.Location);
        Assert.AreEqual("2024-02-29", result.Date);
    }

    [Test]
    public void test_future_date_beyond_allowed_range()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetWeatherForecast("Boston", "2030-01-01"));
        Assert.AreEqual("Date is beyond the allowed forecast range.", ex.Message);
    }
}