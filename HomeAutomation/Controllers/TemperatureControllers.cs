namespace HomeAutomation.Controllers;

public interface ITemperatureController
{
    double GetTargetTemperature();
}

public class DummyTemperatureController : ITemperatureController
{
    public double GetTargetTemperature()
    {
        return 24.0;
    }
}