namespace HomeAutomation.Controllers;

public interface ITemperatureController
{
    double GetTargetTemperature();
}

public class SimpleTemperatureController(double targetTemperature) : ITemperatureController
{
    private readonly double _targetTemperature = targetTemperature;

    public double GetTargetTemperature()
    {
        return _targetTemperature;
    }
}