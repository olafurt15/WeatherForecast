using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Models
{
    public class ForecastEntry
    {
        public string Time { get; set; }
        public string WindSpeed { get; set; }
        public string Temp { get; set; }
        public string WeatherDescription { get; set; }
        public string WindDirection { get; set; }
    }
}
