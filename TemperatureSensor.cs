using Iot.Device.DHTxx;
using Iot.Units;

namespace BugzapperLabs.Temperatured
{
    public class TemperatureSensor
    {
        private readonly DhtBase _sensor;

        public TemperatureSensor()
        {
            // noop to allow null sensor
        }
        public TemperatureSensor(DhtBase sensor)
        {
            _sensor = sensor;
        }

        public Temperature Temperature => _sensor?.Temperature ?? Temperature.FromFahrenheit(GetFakeTemperature());
        public double Humidity => _sensor?.Humidity ?? GetFakeHumidity();
        public bool IsLastReadSuccessful => _sensor?.IsLastReadSuccessful ?? true;

        private readonly double[] _fakeTemperatures = {73.0, 32.0, 73.0, 73.0, 73.0, 73.0, 73.0, 32.0, 73.0, 73.0};
        private readonly double[] _fakeHumidity = {48.0, 10.0, 48.0, 48.0, 48.0, 48.0, 48.0, 10.0, 48.0, 48.0};
        private int _fakeTempIndex;
        private int _fakeHumidityIndex;
        private double GetFakeTemperature()
        {
            if (_fakeTempIndex >= _fakeTemperatures.Length)
            {
                _fakeTempIndex = 0;
            }

            return _fakeTemperatures[_fakeTempIndex++];
        }
        
        private double GetFakeHumidity()
        {
            if (_fakeHumidityIndex >= _fakeHumidity.Length)
            {
                _fakeHumidityIndex = 0;
            }

            return _fakeHumidity[_fakeHumidityIndex++];
        }
    }
}