using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Controllers;
using YourNamespace.Models;
using YourNamespace.Services;

[TestFixture]
public class WeatherForecastControllerTests
{
    private WeatherForecastController _controller;
    private Mock<IWeatherForecastService> _mockService;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IWeatherForecastService>();
        _controller = new WeatherForecastController(_mockService.Object);
    }

    [Test]
    public void Test_RetrieveAllWeatherForecasts_ValidService()
    {
        // Arrange
        var forecasts = new List<WeatherForecast> { new WeatherForecast() };
        _mockService.Setup(service => service.GetAllForecasts()).Returns(forecasts);

        // Act
        var result = _controller.GetAllForecasts();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(forecasts, okResult.Value);
    }

    [Test]
    public void Test_CreateNewWeatherForecast_ValidInput()
    {
        // Arrange
        var forecast = new WeatherForecast { /* valid data */ };
        _mockService.Setup(service => service.CreateForecast(forecast)).Returns(forecast);

        // Act
        var result = _controller.CreateForecast(forecast);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result);
        var createdResult = result as CreatedAtActionResult;
        Assert.AreEqual(forecast, createdResult.Value);
    }

    [Test]
    public void Test_CreateNewWeatherForecast_NullInput()
    {
        // Arrange
        WeatherForecast forecast = null;

        // Act
        var result = _controller.CreateForecast(forecast);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public void Test_CreateNewWeatherForecast_InvalidData()
    {
        // Arrange
        var forecast = new WeatherForecast { /* missing required fields */ };

        // Act
        var result = _controller.CreateForecast(forecast);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public void Test_RetrieveAllWeatherForecasts_EmptyList()
    {
        // Arrange
        var forecasts = new List<WeatherForecast>();
        _mockService.Setup(service => service.GetAllForecasts()).Returns(forecasts);

        // Act
        var result = _controller.GetAllForecasts();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.AreEqual(forecasts, okResult.Value);
    }

    [Test]
    public void Test_ErrorHandling_RetrieveForecast_InvalidId()
    {
        // Arrange
        int invalidId = -1;

        // Act
        var result = _controller.GetForecastById(invalidId);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public void Test_ErrorHandling_RetrieveForecast_NonExistentId()
    {
        // Arrange
        int nonExistentId = 999;
        _mockService.Setup(service => service.GetForecastById(nonExistentId)).Returns((WeatherForecast)null);

        // Act
        var result = _controller.GetForecastById(nonExistentId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}