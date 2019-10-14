using Microsoft.AspNetCore.Mvc;

namespace BugzapperLabs.Temperatured
{
    public class DataController: Controller
    {
        private readonly TemperatureService _temperatureService;

        public DataController(TemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }
        
        [HttpGet]
        [Route("temperature")]
        public IActionResult GetTemperature()
        {
            var temperature = _temperatureService.Fahrenheit;
            if (temperature == double.MinValue)
            {
                return Problem("No temperature readings yet. Try again later.");
            }
            
            return Ok(temperature);
        }
        
        [HttpGet]
        [Route("humidity")]
        public IActionResult GetHumidity()
        {
            var humidity = _temperatureService.Humidity;
            if (humidity == double.MinValue)
            {
                return Problem("No humidity readings yet. Try again later.");
            }
            
            return Ok(humidity);
        }
    }
}