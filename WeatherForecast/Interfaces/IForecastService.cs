using WeatherForecast.Models;

namespace WeatherForecast.Interfaces
{
    public interface IForecastService
    {
        /// <summary>
        /// Gets a complete forecast for a given weather station
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns>An object containing Station name, last updated and all forecast entries for the station</returns>
        Task<Forecast> GetForecastForStation(string stationId);
    }
}
