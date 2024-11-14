using HomeAutomation.Simulation;

namespace HomeAutomation.CurrentTemp;

public interface ICurrTemperatureProvider
{
    double GetCurrTemperature();
}

public class PerfectTemperatureSensor(Room room) : ICurrTemperatureProvider
{
    private readonly Room _room = room;

    public double GetCurrTemperature()
    {
        return _room.Temperature;
    }

    public void SetCalibrationFactor(double factor)
    {
        // Pretend to set calibration factor...
    }

    public void SetCalibrationOffset(double offset)
    {
        // Pretend to set calibration offset...
    }
}

public class NoisyTemperatureSensor(Room room) : ICurrTemperatureProvider
{
    private readonly Room _room = room;
    private readonly Random _random = new();

    public double GetCurrTemperature()
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