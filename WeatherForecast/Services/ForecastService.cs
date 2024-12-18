using System.Net.Http.Headers;
using System.Xml;
using WeatherForecast.Interfaces;
using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public class ForecastService : IForecastService
    {
        private readonly HttpClient _client;
        public ForecastService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"https://xmlweather.vedur.is/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/xml"));
        }

        //Could be in a seperate file
        private Dictionary<string, string> _windDirectionTokens = new Dictionary<string, string>
        {
            ["Logn"] = "Logn",
            ["N"] = "Norðan",
            ["NNA"] = "Norð-norð-austan",
            ["ANA"] = "Aust-norð-austan",
            ["A"] = "Austan",
            ["ASA"] = "Aust-suð-austan",
            ["SA"] = "Suð-austan",
            ["SSA"] = "Suð-suð-austan",
            ["S"] = "Sunnan",
            ["SSV"] = "Suð-suð-vestan",
            ["SV"] = "Suð-vestan",
            ["VSV"] = "Vest-suð-vestan",
            ["V"] = "Vestan",
            ["VNV"] = "Vest-norð-vestan",
            ["NV"] = "Norð-vestan",
            ["NNV"] = "Norð-norð-vestan"
        };


        public async Task<Forecast> GetForecastForStation(int stationId)
        {
            var response = await GetForecast(stationId);
            var xmlDoc = new XmlDocument();
            var responseString = response.Content.ReadAsStringAsync();
            xmlDoc.LoadXml(responseString.Result);

            // These strings should be in Constants
            var stationNode = xmlDoc.SelectSingleNode("//forecasts/station");
            var stationName = stationNode?.SelectSingleNode("name")?.InnerXml ?? "";
            var forecastLastUpdateTime = stationNode?.SelectSingleNode("atime")?.InnerXml ?? "";
            var fcNodes = stationNode?.SelectNodes("forecast");

            Forecast forecast = new Forecast()
            {
                Station = stationName,
                LastUpdated = forecastLastUpdateTime,
                ForecastEntries = new List<ForecastEntry>()
            };

            foreach (XmlNode node in fcNodes)
            {
                forecast.ForecastEntries.Add(new ForecastEntry
                {
                    Time = node.SelectSingleNode("ftime")?.InnerText?.ToString() ?? "",
                    WindSpeed = node.SelectSingleNode("F")?.InnerText?.ToString() ?? "",
                    Temp = node.SelectSingleNode("T")?.InnerText?.ToString() ?? "",
                    WeatherDescription = node.SelectSingleNode("W")?.InnerText?.ToString() ?? "",
                    WindDirection = GetWindDirectionForNode(node.SelectSingleNode("D")?.InnerText?.ToString() ?? ""),
                });
            }
            return forecast;
        }
        private async Task<HttpResponseMessage> GetForecast(int stationId)
        {
            return await _client.GetAsync($"?op_w=xml&type=forec&lang=is&view=xml&ids={stationId}");
        }

        private string GetWindDirectionForNode(string nodeValue)
        {
            if (_windDirectionTokens.ContainsKey(nodeValue))
                return _windDirectionTokens[nodeValue];

            return nodeValue;
        }
    }
}
