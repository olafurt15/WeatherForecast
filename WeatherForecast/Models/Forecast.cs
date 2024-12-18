using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Models
{
    public class Forecast
    {
        public string Station { get; set; } = "";
        public string LastUpdated { get; set; } = "";
        public List<ForecastEntry>? ForecastEntries { get; set; }
    }
}
