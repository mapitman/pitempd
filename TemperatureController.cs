using Microsoft.AspNetCore.Mvc;

namespace BugzapperLabs.Temperatured
{
    public class TemperatureController: Controller
    {
        [HttpGet]
        [Route("temperature")]
        public IActionResult Get()
        {
            return Ok(new
            {
                Current = 73.2,
                Low = 71.3,
                High = 75.6
            });
        }
    }
}