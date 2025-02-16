using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Source.Services;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Source.Tests
{
    public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ProgramTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public void Services_AreRegisteredCorrectly()
        {
            // Arrange
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();

            // Act
            using var scope = scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetService<IWeatherForecastService>();

            // Assert
            Assert.NotNull(service);
            Assert.IsType<WeatherForecastService>(service);
        }
    }
}
