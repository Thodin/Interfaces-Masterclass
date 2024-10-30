using HomeAutomation.Simulation;

namespace HomeAutomation.Sensors;

public interface ITemperatureProvider
{
    double GetCurTemperature();
}

public class PerfectTemperatureSensor(Room room) : ITemperatureProvider
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

public class NoisyTemperatureSensor(Room room) : ITemperatureProvider
{
    private readonly Room _room = room;
    private readonly Random _random = new();

    public double GetCurTemperature()
    {
        // Add noise between -1.0 and 1.0 to the signal.
        return _room.Temperature + _random.NextDouble() * 2.0 - 1.0;
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