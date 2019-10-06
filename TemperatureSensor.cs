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
        public double Humidity => _sensor?.Humidity ?? 45.0;
        public bool IsLastReadSuccessful => _sensor?.IsLastReadSuccessful ?? true;

        private readonly double[] _fakeTemperatures = {73.0, 32.0, 73.0, 73.0, 73.0, 73.0, 73.0, 32.0, 73.0, 73.0};
        private int _fakeIndex;
        private double GetFakeTemperature()
        {
            if (_fakeIndex >= _fakeTemperatures.Length)
            {
                _fakeIndex = 0;
            }

            return _fakeTemperatures[_fakeIndex++];
        }
    }
}