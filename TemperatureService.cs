using System;
using System.Collections.Generic;
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

        internal void ReadData()
        {
            try
            {
                var temp = _sensor.Temperature.Fahrenheit;
                var humidity = _sensor.Humidity;
                if (_sensor.IsLastReadSuccessful)
                {
                    _tempSeries.Add(temp);
                    _humiditySeries.Add(humidity);
                }
                _tempSeries.EnsureMaxCapacity(_capacity);
                _humiditySeries.EnsureMaxCapacity(_capacity);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
        }
    }
}