namespace HomeAutomation.Actuators;

public interface IHeatProvider
{
    void SetHeatingPower(double wattage);
    void TurnOn();
    void TurnOff();
    bool IsOn();
}

public class SimpleHeater : IHeatProvider
{
    private bool _isOn = false;
    private double _power = 0.0;
    const double MAX_POWER = 2000.0;

    public void SetHeatingPower(double wattage)
    {
        if (wattage < 0.0)
        {
            _power = 0.0;
        }
        else
        {
            _power = Math.Min(wattage, MAX_POWER);
        }
    }

    public void TurnOff()
    {
        _isOn = false;
    }

    public void TurnOn()
    {
        _isOn = true;
    }

    public bool IsOn()
    {
        return _isOn;
    }

    public double GetCurPower()
    {
        if (!_isOn) { return 0.0; }
        return _power;
    }

    public void TurnOnForDuration(TimeSpan duration)
    {
        // Some implementation...
    }
}