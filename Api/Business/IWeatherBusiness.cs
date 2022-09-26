using System.Threading.Tasks;

namespace Api.Business
{
    public interface IWeatherBusiness
    {
        #region Public Methods

        Task<WeatherDto> GetWeatherAsync(string city);

        #endregion Public Methods
    }
}
