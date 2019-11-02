using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BugzapperLabs.Temperatured
{
    public class TemperatureServiceWorker : BackgroundService
    {
        private readonly ILogger<TemperatureServiceWorker> _logger;
        private readonly TemperatureService _temperatureService;

        public TemperatureServiceWorker(ILogger<TemperatureServiceWorker> logger, TemperatureService temperatureService)
        {
            _logger = logger;
            _temperatureService = temperatureService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _temperatureService.ReadDataAsync(stoppingToken);
            }
        }
    }
}