// See https://aka.ms/new-console-template for more information
using WeatherForecast.Services;
using WeatherForecast.Models;

Dictionary<string, int> stations = new Dictionary<string, int>
{
    ["reykjavík"] = 1,
    ["akureyri"] = 3471
};

var forecastService = new ForecastService();

while (true)
{
    Console.WriteLine($"Stöðvalisti: \n ");
    foreach(var key in stations.Keys)
    {
        Console.WriteLine(key);
    }

    Console.WriteLine("Veldu stöð eða x til að hætta");

    string userSelectedStation = Console.ReadLine()?.ToLower() ?? "";

    if (string.IsNullOrWhiteSpace(userSelectedStation)){
        //do nothing
    }
    else if(userSelectedStation.ToLower() == "x")
    {
        Environment.Exit(0);
    }
    else if (string.IsNullOrWhiteSpace(userSelectedStation) || !stations.ContainsKey(userSelectedStation))
    {
        Console.WriteLine("stöð fannst ekki");
    }
    else
    {
        Forecast forecast = forecastService.GetForecastForStation(stations[userSelectedStation]).Result;
        Console.WriteLine($"Veðurspá fyrir {forecast.Station} næstu daga:  (síðast uppfært) {forecast.LastUpdated}");

        if (forecast.ForecastEntries == null || forecast.ForecastEntries?.Count == 0)
        {
            Console.WriteLine($"Engar spár fundust fyrir veðurstöð {forecast.Station}");
        }
        else
        {
            foreach (ForecastEntry forecastEntry in forecast.ForecastEntries)
            {
                Console.WriteLine("---");
                Console.WriteLine($"{forecastEntry.Time} \n Lýsing: {forecastEntry.WeatherDescription} \n Hiti: {forecastEntry.Temp} \n Vindhraði: {forecastEntry.WindSpeed} \n Vindátt: {forecastEntry.WindDirection} ");
            }
        }
    }
}