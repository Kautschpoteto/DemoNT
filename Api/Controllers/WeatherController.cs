using Api.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        #region Private Fields

        private readonly ISimpleCache<WeatherDto> _cacheBusiness;
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherBusiness _weatherBusiness;

        #endregion Private Fields

        #region Public Constructors

        public WeatherController(ILogger<WeatherController> logger, IWeatherBusiness weatherBusiness, ISimpleCache<WeatherDto> cacheBusiness)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weatherBusiness = weatherBusiness ?? throw new ArgumentNullException(nameof(weatherBusiness));
            _cacheBusiness = cacheBusiness ?? throw new ArgumentNullException(nameof(cacheBusiness));
        }

        #endregion Public Constructors

        #region Public Methods

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet]
        public async Task<IActionResult> Get(string city = "")
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("Stadt fehlt");
            }

            WeatherDto weather = _cacheBusiness.Get(city);

            if (weather == null)
            {
                try
                {
                    weather = await _weatherBusiness.GetWeatherAsync(city);
                }catch(Exception e)
                {
                    if (e.Message.Contains("404"))
                    {
                        _logger.LogError($"Stadt {city} nicht gefunden");
                        return Ok(new WeatherDto($"Stadt {city} nicht gefunden"));
                    }

                    _logger.LogError(e.Message);
                    return Ok(new WeatherDto($"Fehler: {e.Message}"));
                }
                _cacheBusiness.Set(city,weather);
            }

            return Ok(weather);
        }

        #endregion Public Methods
    }
}
