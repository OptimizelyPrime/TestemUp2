using NUnit.Framework;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyApi.Controllers;
using MyApi.Models;

[TestFixture]
public class WeatherForecastControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        // Initialize HttpClient or any other setup needed before each test
        _client = new HttpClient(); // Assuming you have a way to set up your client
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up resources after each test if necessary
        _client.Dispose();
    }

    [Test]
    public async Task test_normal_usage_get_all_forecasts()
    {
        // Arrange
        var request = "GET /WeatherForecast";

        // Act
        var response = await _client.GetAsync(request);

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
        var request = "GET /WeatherForecast/1";

        // Act
        var response = await _client.GetAsync(request);

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
        var request = "POST /WeatherForecast";
        var body = new StringContent(JsonConvert.SerializeObject(new { Id = 3, Date = "2023-10-01", TemperatureC = 20, Summary = "Sunny" }), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(request, body);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        var forecast = JsonConvert.DeserializeObject<WeatherForecast>(content);
        Assert.IsNotNull(forecast);
        Assert.AreEqual(3, forecast.Id);
    }

    [Test]
    public async Task test_edge_case_get_forecast_by_id_not_exist()
    {
        // Arrange
        var request = "GET /WeatherForecast/999";

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task test_edge_case_post_forecast_missing_required_fields()
    {
        // Arrange
        var request = "POST /WeatherForecast";
        var body = new StringContent(JsonConvert.SerializeObject(new { Id = 4, TemperatureC = 25 }), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(request, body);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_get_forecast_invalid_id_type()
    {
        // Arrange
        var request = "GET /WeatherForecast/abc";

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_post_null_forecast()
    {
        // Arrange
        var request = "POST /WeatherForecast";
        var body = new StringContent("null", Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(request, body);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_get_all_forecasts_service_down()
    {
        // Arrange
        var request = "GET /WeatherForecast";
        // Simulate service down scenario here if possible

        // Act
        var response = await _client.GetAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}