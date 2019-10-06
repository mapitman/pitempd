using Microsoft.AspNetCore.Mvc;

namespace BugzapperLabs.Temperatured
{
    public class TemperatureController: Controller
    {
        [HttpGet]
        [Route("temperature/current")]
        public IActionResult Current()
        {
            return Ok(73.2);
        }
        
        [HttpGet]
        [Route("temperature/low")]
        public IActionResult Low()
        {
            return Ok(71.1);
        }
        
        [HttpGet]
        [Route("temperature/high")]
        public IActionResult High()
        {
            return Ok(75.7);
        }
    }
}