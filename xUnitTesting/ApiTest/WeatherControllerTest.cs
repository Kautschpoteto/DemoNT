using Api;
using Api.Business;
using Api.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;

namespace xUnitTesting
{
    public class WeatherControllerTest
    {
        #region Private Fields

        private readonly WeatherDto _data = new WeatherDto
        {
            Temperature = 4,
            TemperatureMin = 1,
            TemperatureMax = 5,
            AirPressure = 1,
            Humidity = 100,
            WindSpeed = 1,
            WindDirection = "West",
            CloudCoverCondition = 1,
            City = "city"
        };

        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherBusiness _weatherBusiness;
        private readonly ISimpleCache<WeatherDto> _weatherCacheBusiness;

        #endregion Private Fields

        #region Public Constructors

        public WeatherControllerTest()
        {
            _weatherBusiness = A.Fake<IWeatherBusiness>();
            _logger = A.Fake<ILogger<WeatherController>>();
            _weatherCacheBusiness = A.Fake<ISimpleCache<WeatherDto>>();
        }

        #endregion Public Constructors

        #region Public Methods

        [Fact]
        public void ControllerCtr_logger_null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new WeatherController(null, _weatherBusiness, _weatherCacheBusiness)
            );
        }

        [Fact]
        public void ControllerCtr_WeatherBusiness_null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new WeatherController(_logger, null, _weatherCacheBusiness)
            );
        }

        [Fact]
        public void ControllerCtr_WeatherCacheBusiness_null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new WeatherController(_logger, _weatherBusiness, null)
            );
        }

        [Fact]
        public async Task Get_EmptyString_returns_BadRequest()
        {
            var controller = new WeatherController(_logger, _weatherBusiness, _weatherCacheBusiness);

            IActionResult actionResult = await controller.Get(string.Empty);

            Assert.NotNull(actionResult);
            var badReqResult = actionResult as BadRequestObjectResult;

            Assert.NotNull(badReqResult);
            Assert.Equal(400, badReqResult.StatusCode);
        }

        [Fact]
        public async Task Get_NullString_returns_BadRequest()
        {
            var controller = new WeatherController(_logger, _weatherBusiness, _weatherCacheBusiness);

            IActionResult actionResult = await controller.Get(null);

            Assert.NotNull(actionResult);
            var badReqResult = actionResult as BadRequestObjectResult;

            Assert.NotNull(badReqResult);
            Assert.Equal(400, badReqResult.StatusCode);
        }

        [Fact]
        public async Task Get_withCache_returns_ok()
        {
            A.CallTo(() => _weatherCacheBusiness
                    .Get("city"))
                .Returns(_data);

            var controller = new WeatherController(_logger, _weatherBusiness, _weatherCacheBusiness);

            IActionResult actionResult = await controller.Get("city");

            Assert.NotNull(actionResult);

            var okResult = actionResult as OkObjectResult;
            var weatherDto = okResult.Value as WeatherDto;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.NotNull(okResult.Value);
            Assert.IsType<WeatherDto>(weatherDto);
            Assert.Equal("city", weatherDto.City);
        }

        [Fact]
        public async Task Get_withOutCache_returns_ok()
        {
            A.CallTo(() => _weatherBusiness
                    .GetWeatherAsync("city"))
                .Returns(Task.FromResult(_data));

            A.CallTo(() => _weatherCacheBusiness
                    .Get("city"))
                .Returns(null);

            var controller = new WeatherController(_logger, _weatherBusiness, _weatherCacheBusiness);

            IActionResult actionResult = await controller.Get("city");

            Assert.NotNull(actionResult);

            var okResult = actionResult as OkObjectResult;
            var weatherDto = okResult.Value as WeatherDto;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.NotNull(okResult.Value);
            Assert.IsType<WeatherDto>(weatherDto);
            Assert.Equal("city", weatherDto.City);
        }

        #endregion Public Methods
    }
}
