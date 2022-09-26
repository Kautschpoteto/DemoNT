using System;

namespace Api
{
    public class WeatherDto
    {
        #region Public Constructors

        public WeatherDto()
        {
            Date = DateTime.Now;            
        }

        public WeatherDto(string error)
        {
            Date = DateTime.Now;
            Error = error;
        }

        #endregion Public Constructors

        #region Public Properties
        public string Error { get; set; }
        public double AirPressure { get; set; }
        public string City { get; set; }
        public double CloudCoverCondition { get; set; }
        public DateTime Date { get; set; }
        public double Humidity { get; set; }
        public int Temperature { get; set; }
        public int TemperatureMax { get; set; }
        public int TemperatureMin { get; set; }
        public string WindDirection { get; set; }
        public double WindSpeed { get; set; }

        #endregion Public Properties
    }
}
