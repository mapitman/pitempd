using System;
using System.Threading;
using System.Threading.Tasks;
using BugzapperLabs.Temperatured;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus;

namespace pitempd
{
    public class TemperatureWorker : BackgroundService
    {
        private readonly ILogger<TemperatureWorker> _logger;
        private readonly TemperatureService _temperatureService;

        public TemperatureWorker(
            ILogger<TemperatureWorker> logger,
            TemperatureService temperatureService)
        {
            _logger = logger;
            _temperatureService = temperatureService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tempGauge = Metrics.CreateGauge("temperature", "Current temperature");
            var humidityGauge = Metrics.CreateGauge("humidity", "Current humidity");
            while (!stoppingToken.IsCancellationRequested)
            {
                var temperature = _temperatureService.Fahrenheit;
                if (!double.IsNaN(temperature))
                {
                    tempGauge.Set(temperature);
                }
                
                var humidity = _temperatureService.Humidity;
                if (!double.IsNaN(humidity))
                {
                    humidityGauge.Set(humidity);
                }
                _logger.LogInformation("Temp: {temp} Humi: {humidity}", temperature, humidity);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
