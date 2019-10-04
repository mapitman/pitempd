using Iot.Device.DHTxx;
using Iot.Units;

namespace BugzapperLabs.Temperatured
{
    public class TemperatureSensor
    {
        private readonly DhtBase _sensor;

        public TemperatureSensor(DhtBase sensor)
        {
            _sensor = sensor;
        }

        public Temperature Temperature => _sensor?.Temperature ?? Temperature.FromFahrenheit(73.0);
        public double Humidity => _sensor?.Humidity ?? 45.0;
        public bool IsLastReadSuccessful => _sensor?.IsLastReadSuccessful ?? true;
    }
}