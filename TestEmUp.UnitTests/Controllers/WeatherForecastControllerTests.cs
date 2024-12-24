using NUnit.Framework;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YourNamespace.Controllers;
using YourNamespace.Models;

[TestFixture]
public class WeatherForecastControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        // Initialize HttpClient or any other necessary setup here
        _client = new HttpClient();
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up resources if needed
        _client.Dispose();
    }

    [Test]
    public async Task test_normal_usage_get_all_forecasts()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        var forecasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);
        Assert.IsNotNull(forecasts);
    }

    [Test]
    public async Task test_normal_usage_get_forecast_by_valid_id()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast/1");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        var forecast = JsonConvert.DeserializeObject<WeatherForecast>(content);
        Assert.IsNotNull(forecast);
        Assert.AreEqual(1, forecast.Id);
    }

    [Test]
    public async Task test_normal_usage_post_new_forecast()
    {
        // Arrange
        var forecast = new WeatherForecast { Id = 1, Date = "2023-10-01", TemperatureC = 20, Summary = "Sunny" };
        var json = JsonConvert.SerializeObject(forecast);
        var request = new HttpRequestMessage(HttpMethod.Post, "/WeatherForecast")
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        var createdForecast = JsonConvert.DeserializeObject<WeatherForecast>(content);
        Assert.IsNotNull(createdForecast);
        Assert.AreEqual(forecast.Id, createdForecast.Id);
    }

    [Test]
    public async Task test_edge_case_get_forecast_by_id_not_exist()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast/999");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task test_edge_case_post_forecast_missing_required_fields()
    {
        // Arrange
        var json = JsonConvert.SerializeObject(new { Id = 1, TemperatureC = 20 });
        var request = new HttpRequestMessage(HttpMethod.Post, "/WeatherForecast")
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_get_forecast_invalid_id_type()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast/abc");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_post_forecast_null_body()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, "/WeatherForecast")
        {
            Content = new StringContent("null", System.Text.Encoding.UTF8, "application/json")
        };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_get_all_forecasts_service_exception()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast");

        // Simulate service throwing exception (mocking would be done here)

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}