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
            var tempGauge = Metrics.CreateGauge("temperature_current", "Current temperature");
            while (!stoppingToken.IsCancellationRequested)
            {
                var temperature = _temperatureService.Fahrenheit;
                if (temperature != double.MinValue)
                {
                    tempGauge.Set(temperature);
                }
                _logger.LogInformation("Time: {time} Temperature: {temp}", DateTimeOffset.Now, temperature);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
