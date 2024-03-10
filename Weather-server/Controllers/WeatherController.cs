using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Weather_server.Models.Backend;
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
            var weathers = await _parserService.ParseFiles(files);
            if (weathers.Count == 0)
            {
                return BadRequest("No weather data found in the files.");
            }
            await _weatherService.AddRangeAsync(weathers);
            return Ok("Files uploaded successfully");
        }


        [HttpPut("getWeatherWithDate")]
        public async Task<IActionResult> GetWeatherWithDate([FromBody] WeatherDateModel weatherDateModel)
        {
            try
            {
                DateTime utcDate = new(weatherDateModel.Date.Year, weatherDateModel.Date.Month, weatherDateModel.Date.Day, 0, 0, 0, DateTimeKind.Utc);
                var weathers = await _weatherService.GetAllAsync(utcDate);
                _logger.LogInformation("Weather data retrieved successfully for the specified date.");
                var weatherModels = _mapper.Map<List<WeatherModel>>(weathers);
                return Ok(weatherModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving weather data.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}