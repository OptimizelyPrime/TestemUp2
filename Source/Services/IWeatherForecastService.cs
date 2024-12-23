public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> GetForecasts();
    WeatherForecast GetForecastById(int id);
    void AddForecast(WeatherForecast forecast);
}

public class WeatherForecastService : IWeatherForecastService
{
    private readonly List<WeatherForecast> _forecasts = new();

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        return _forecasts;
    }

    public WeatherForecast GetForecastById(int id)
    {
        return _forecasts.FirstOrDefault(f => f.Id == id);
    }

    public void AddForecast(WeatherForecast forecast)
    {
        _forecasts.Add(forecast);
    }
}
