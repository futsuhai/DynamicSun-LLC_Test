using Microsoft.AspNetCore.Mvc;

namespace Weather_server.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(
            ILogger<WeatherController> logger
        )
        {
            _logger = logger;
        }

        [HttpGet("ping")]
        public async Task<IActionResult> Ping()
        {
            try
            {
                _logger.LogInformation("Successfully");
                return Ok("Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}