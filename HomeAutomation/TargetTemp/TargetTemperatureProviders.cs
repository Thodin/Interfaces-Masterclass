namespace HomeAutomation.TargetTemp;

public interface ITargetTemperatureProvider
{
    double GetTargetTemperature();
}

public class SimpleTargetTemperatureProvider(double targetTemperature) : ITargetTemperatureProvider
{
    private readonly double _targetTemperature = targetTemperature;

    public double GetTargetTemperature()
    {
        return _targetTemperature;
    }
}