using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace BugzapperLabs.Temperatured
{
    public class TemperatureService
    {
        private readonly object _locker = new object();
        private readonly ILogger<TemperatureSensor> _logger;
        private readonly TemperatureSensor _sensor;
        private readonly IList<double> _tempSeries;

        public TemperatureService(TemperatureSensor sensor, ILogger<TemperatureSensor> logger)
        {
            _sensor = sensor;
            _logger = logger;
            _tempSeries = new List<double>(20);
        }

        public double Fahrenheit => _tempSeries.FilteredAverage(2.0);

        private void ReadData(object state)
        {
            try
            {
                var temp = _sensor.Temperature.Fahrenheit;
                if (_sensor.IsLastReadSuccessful) _tempSeries.Add(temp);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
        }
    }
}