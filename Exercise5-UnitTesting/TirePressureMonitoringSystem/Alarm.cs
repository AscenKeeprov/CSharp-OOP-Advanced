public class Alarm
{
    private const double LowPressureThreshold = 17;
    private const double HighPressureThreshold = 21;

    readonly Sensor _sensor = new Sensor();

    bool _alarmOn = false;

    public bool AlarmOn { get { return _alarmOn; } }

    public void Check()
    {
	double psiPressureValue = _sensor.PopNextPressurePsiValue();
	if (psiPressureValue < LowPressureThreshold ||
	    HighPressureThreshold < psiPressureValue)
	    _alarmOn = true;
    }
}
