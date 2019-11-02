using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BugzapperLabs.Temperatured
{
    public class TemperatureService
    {
        private readonly ILogger<TemperatureSensor> _logger;
        private readonly TemperatureSensor _sensor;
        private readonly IList<double> _tempSeries;
        private readonly IList<double> _humiditySeries;
        private readonly int _capacity;

        public TemperatureService(TemperatureSensor sensor, ILogger<TemperatureSensor> logger)
        {
            _sensor = sensor;
            _logger = logger;
            _capacity = 20;
            _tempSeries = new List<double>(_capacity);
            _humiditySeries = new List<double>(_capacity);
        }

        public double Fahrenheit => _tempSeries.FilteredAverage(1.0);

        public double Humidity => _humiditySeries.FilteredAverage(1.0);

        internal async Task ReadDataAsync(CancellationToken stoppingToken)
        {
            var temp = await ReadTemperatureAsync(stoppingToken);
            if (!double.IsNaN(temp))
            {
                _tempSeries.Add(temp);    
            }
            
            var humidity = await ReadHumidityAsync(stoppingToken);
            if (!double.IsNaN(humidity))
            {
                _humiditySeries.Add(humidity);
            }
            
            _logger.LogDebug($"Raw temp: {temp}  Raw humi: {humidity}");
            _tempSeries.EnsureMaxCapacity(_capacity);
            _humiditySeries.EnsureMaxCapacity(_capacity);
        }

        private async Task<double> ReadTemperatureAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            var result = double.NaN;
            try
            {
                var temp = _sensor.Temperature.Fahrenheit;
                if (_sensor.IsLastReadSuccessful)
                {
                    result = temp;

                }
                else
                {
                    _logger.LogDebug("Failed to read from sensor.");
                }
                
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return result;
        }
        internal async Task<double> ReadHumidityAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            var result = double.NaN;
            try
            {
                var humidity = _sensor.Humidity;
                if (_sensor.IsLastReadSuccessful)
                {
                    result = humidity;
                }
                else
                {
                    _logger.LogDebug("Failed to read from sensor.");
                }
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return result;
        }
    }
}