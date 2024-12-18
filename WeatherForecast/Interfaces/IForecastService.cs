using WeatherForecast.Models;

namespace WeatherForecast.Interfaces
{
    public interface IForecastService
    {
        Task<Forecast> GetForecastForStation(int stationId);
    }
}
