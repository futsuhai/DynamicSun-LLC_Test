using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using Weather_server.Models.Backend;
using Weather_server.Models.Client;
using Weather_server.Services.DayService;

namespace Weather_server.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService, IMapper mapper) : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger = logger;
        private readonly IWeatherService _weatherService = weatherService;
        private readonly IMapper _mapper = mapper;

        [HttpPost("createWeathersFromFiles")]
        public async Task<IActionResult> CreateWeathersFromFiles(IFormFile[] files)
        {
            List<string> firstCellValues = [];

            foreach (var file in files)
            {
                // Чтение содержимого файла
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;

                // Загрузка книги из потока
                IWorkbook workbook = WorkbookFactory.Create(memoryStream);

                // Получение первого листа
                ISheet sheet = workbook.GetSheetAt(0);

                if (sheet != null && sheet.GetRow(0) != null && sheet.GetRow(0).GetCell(0) != null)
                {
                    // Чтение значения первой ячейки
                    var cellValue = sheet.GetRow(0).GetCell(0).ToString();
                    firstCellValues.Add(cellValue);
                }
            }

            foreach (var value in firstCellValues)
            {
                _logger.LogInformation($"First cell value: {value}");
            }

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

        [HttpPost("createWeather")]
        public async Task<IActionResult> CreateWeather([FromBody] WeatherModel weatherModel)
        {
            try
            {
                var weather = _mapper.Map<Weather>(weatherModel);
                await _weatherService.AddAsync(weather);
                _logger.LogInformation("Weather successfully created");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creting weather");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("createWeatherRange")]
        public async Task<IActionResult> CreateWeatherRange([FromBody] WeatherModel[] weatherModel)
        {
            try
            {
                var weathers = _mapper.Map<List<Weather>>(weatherModel);
                await _weatherService.AddRangeAsync(weathers);
                _logger.LogInformation("Weathers successfully created");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creting weathers");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}