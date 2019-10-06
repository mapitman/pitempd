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
        private int _capacity;

        public TemperatureService(TemperatureSensor sensor, ILogger<TemperatureSensor> logger)
        {
            _sensor = sensor;
            _logger = logger;
            _capacity = 20;
            _tempSeries = new List<double>(_capacity);
        }

        public double Fahrenheit
        {
            get
            {
                _logger.LogInformation("Series count: {count}", _tempSeries.Count);
                return _tempSeries.FilteredAverage(1.0);
            }
        }

        internal void ReadData()
        {
            try
            {
                var temp = _sensor.Temperature.Fahrenheit;
                if (_sensor.IsLastReadSuccessful) _tempSeries.Add(temp);
                _tempSeries.EnsureMaxCapacity(_capacity);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
        }
    }
}