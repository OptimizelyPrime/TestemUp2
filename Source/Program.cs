/// <summary>
/// Dependency on NuGet package 'Microsoft.AspNetCore.App'.
/// </summary>
/// <remarks>
/// This dependency is necessary for building ASP.NET Core applications. It provides the full set of ASP.NET Core features, including MVC, routing, and authentication frameworks. 
/// The version used in this project should be compatible with the current SDK version.
/// </remarks>
/// <summary>
/// Dependency on external library 'System.Diagnostics.CodeAnalysis'.
/// </summary>
/// <remarks>
/// This namespace is necessary for the usage of attributes like 'ExcludeFromCodeCoverage', which indicates to code analysis tools to ignore methods or classes during code coverage analysis.
/// This ensures that certain sections of the codebase do not impact the coverage metrics.
/// Version is determined by the .NET SDK being used.
/// </remarks>
/// <summary>
/// Dependency on project reference 'Source.Services'.
/// </summary>
/// <remarks>
/// This project reference allows access to application services, specifically the application service interfaces and their implementations, such as 'IWeatherForecastService' and 'WeatherForecastService'.
/// These services handle business logic around weather forecasting, needed for the controller actions.
/// </remarks>
internal class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    /// <returns>None.</returns>
    /// <remarks>This method sets up the ASP.NET Core web application.</remarks>
    /// <exception cref="System.Exception">May throw exceptions related to application startup errors.</exception>
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