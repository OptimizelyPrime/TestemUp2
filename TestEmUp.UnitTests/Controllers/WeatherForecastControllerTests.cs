using NUnit.Framework;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

[TestFixture]
public class WeatherForecastControllerTests
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        // Initialize HttpClient or any other setup needed before each test
        _client = new HttpClient();
    }

    [TearDown]
    public void Teardown()
    {
        // Cleanup after each test if necessary
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
        // Additional assertions to check the response content can be added here
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
        // Additional assertions to check the response content can be added here
    }

    [Test]
    public async Task test_normal_usage_post_new_forecast()
    {
        // Arrange
        var content = new StringContent("{\"Id\": 1, \"Date\": \"2023-10-01\", \"TemperatureC\": 20, \"Summary\": \"Sunny\"}", Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "/WeatherForecast") { Content = content };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        // Additional assertions to check the response content can be added here
    }

    [Test]
    public async Task test_edge_case_get_forecast_by_nonexistent_id()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast/999");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task test_edge_case_post_forecast_with_missing_fields()
    {
        // Arrange
        var content = new StringContent("{\"Id\": 1, \"TemperatureC\": 20}", Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "/WeatherForecast") { Content = content };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_get_forecast_with_negative_id()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast/-1");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_post_forecast_with_invalid_json()
    {
        // Arrange
        var content = new StringContent("{\"Id\": \"invalid\", \"Date\": \"2023-10-01\", \"TemperatureC\": \"twenty\", \"Summary\": \"Sunny\"}", Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "/WeatherForecast") { Content = content };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task test_error_handling_get_forecast_with_null_id()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/WeatherForecast/null");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task test_normal_usage_post_forecast_with_valid_data()
    {
        // Arrange
        var content = new StringContent("{\"Id\": 2, \"Date\": \"2023-10-02\", \"TemperatureC\": 25, \"Summary\": \"Cloudy\"}", Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "/WeatherForecast") { Content = content };

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        // Additional assertions to check the response content can be added here
    }
}