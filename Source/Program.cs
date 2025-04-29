using Source.Services;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the entry point of the application and configures the web application.
/// </summary>
/// <remarks>
/// This class contains the startup logic to establish the web application, register services,
/// and configure middleware for handling HTTP requests.
/// </remarks>
[ExcludeFromCodeCoverage]
internal class Program
{
    /// <summary>
    /// Main method, which serves as the entry point for the application.
    /// </summary>
    /// <param name="args">An array of strings that can contain command-line arguments.</param>
    /// <remarks>
    /// The Main method initializes the web application builder, adds necessary services,
    /// and configures the request pipeline for incoming HTTP requests.
    /// </remarks>
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}