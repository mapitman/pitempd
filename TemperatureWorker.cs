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
                if (temperature != double.MinValue)
                {
                    tempGauge.Set(temperature);
                }
                
                var humidity = _temperatureService.Humidity;
                if (humidity != double.MinValue)
                {
                    humidityGauge.Set(humidity);
                }
                _logger.LogInformation("Time: {time} Temperature: {temp} Humidity: {humidity}", DateTimeOffset.Now, temperature, humidity);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
