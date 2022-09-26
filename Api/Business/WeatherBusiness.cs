using System;
using System.Threading.Tasks;

namespace Api.Business
{
    public class WeatherBusiness : IWeatherBusiness
    {
        #region Public Methods

        public async Task<WeatherDto> GetWeatherAsync(string city)
        {
            var openWeatherAPI = new OpenWeatherAPI.OpenWeatherApiClient("efa595f735f4464d96fa58315fc4957e");
            OpenWeatherAPI.QueryResponse query = await openWeatherAPI.QueryAsync(city.Trim() + "&units=metric");

            var data = new WeatherDto
            {
                Temperature = Convert.ToInt32(Math.Round(query.Main.Temperature.CelsiusCurrent / 100)),
                TemperatureMin = Convert.ToInt32(Math.Round(query.Main.Temperature.CelsiusMinimum / 100)),
                TemperatureMax = Convert.ToInt32(Math.Round(query.Main.Temperature.CelsiusMaximum / 100)),
                AirPressure = query.Main.Pressure,
                Humidity = query.Main.Humidity,
                WindSpeed = query.Wind.SpeedMetersPerSecond,
                WindDirection = OpenWeatherAPI.Wind.DirectionEnumToString(query.Wind.Direction),
                CloudCoverCondition = query.Clouds.All,
                City = city.Trim()
            };

            return data;
        }

        #endregion Public Methods
    }
}
