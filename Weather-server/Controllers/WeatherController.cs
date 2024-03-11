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
            var uploadInfoList = new List<FileUploadInfo>();
            try
            {
                foreach (var file in files)
                {
                    var uploadInfo = new FileUploadInfo { FileName = file.FileName };
                    var weathers = await _parserService.ParseFile(file);
                    if (weathers.Count == 0)
                    {
                        _logger.LogError($"Файл {file.FileName} не подлежит разбору");
                        uploadInfo.Result = false;
                    }
                    else
                    {
                        await _weatherService.AddRangeAsync(weathers);
                        uploadInfo.Result = true;
                        _logger.LogInformation($"Данные о погоде из файла {file.FileName} успешно записаны");
                    }
                    uploadInfoList.Add(uploadInfo);
                }
                var uploadInfoModelsList = _mapper.Map<List<FileUploadInfo>>(uploadInfoList);
                return Ok(uploadInfoModelsList);
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