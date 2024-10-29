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

    public void SetCalibrationFactor()
    {
        // Pretend to set calibration factor...
    }

    public void SetCalibrationOffset()
    {
        // Pretend to set calibration offset...
    }
}