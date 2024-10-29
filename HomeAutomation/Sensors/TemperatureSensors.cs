using HomeAutomation.Simulation;

namespace HomeAutomation.Sensors;

public interface ITemperatureProvider
{
    double GetCurTemperature();
}

public class DummyTemperatureSensor(Room room) : ITemperatureProvider
{
    private readonly Room _room = room;

    public double GetCurTemperature()
    {
        return _room.Temperature;
    }
}