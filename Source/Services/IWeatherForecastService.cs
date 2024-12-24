using Source.Models;

namespace Source.Services;


public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> GetForecasts();
    WeatherForecast GetForecastById(int id);
    void AddForecast(WeatherForecast forecast);
}
