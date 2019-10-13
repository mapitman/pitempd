using Microsoft.AspNetCore.Mvc;

namespace BugzapperLabs.Temperatured
{
    public class TemperatureController: Controller
    {
        private readonly TemperatureService _temperatureService;

        public TemperatureController(TemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }
        [HttpGet]
        [Route("temperature")]
        public IActionResult Get()
        {
            var temperature = _temperatureService.Fahrenheit;
            if (temperature == double.MinValue)
            {
                return Problem("No temperature readings yet. Try again later.");
            }
            
            return Ok(temperature);
        }
    }
}