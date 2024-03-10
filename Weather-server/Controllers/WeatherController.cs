using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Weather_server.Models.Client;
using Weather_server.Services.DayService;
using Weather_server.Services.ParserService;

namespace Weather_server.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService, IParserService parserService, IMapper mapper) : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger = logger;
        private readonly IWeatherService _weatherService = weatherService;
        private readonly IParserService _parserService = parserService;
        private readonly IMapper _mapper = mapper;


        [HttpPost("createWeathersFromFiles")]
        public async Task<IActionResult> CreateWeathersFromFiles(IFormFileCollection files)
        {
            try
            {
                var weathers = await _parserService.ParseFiles(files);
                if (weathers.Count == 0)
                {
                    _logger.LogError("Загруженный файл не подлежит разбору");
                    return BadRequest("Загруженный файл не подлежит разбору");
                }
                await _weatherService.AddRangeAsync(weathers);
                _logger.LogInformation("Данные о погоде успешно записаны");
                return Ok("Files uploaded successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Возникла ошибка, во время записи данных о погоде");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("getWeatherWithDate")]
        public async Task<IActionResult> GetWeatherWithDate([FromBody] WeatherDateModel weatherDateModel)
        {
            try
            {
                var utcDate = new DateTime(weatherDateModel.Date.Year, weatherDateModel.Date.Month, weatherDateModel.Date.Day, 0, 0, 0, DateTimeKind.Utc);
                var weathers = await _weatherService.GetAllAsync(utcDate);
                _logger.LogInformation("Успешное получение данных о погоде");
                var weatherModels = _mapper.Map<List<WeatherModel>>(weathers);
                return Ok(weatherModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Возникла ошибка, во сремя получения данных о погоде");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}