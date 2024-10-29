namespace HomeAutomation.Simulation;

public class Room(double initialTemperature)
{
    public double Temperature { get; private set; } = initialTemperature; // [C]
    private double _environmentTemperature = 5; // [C]
    private double _volume = 40; // [m^3]
    private double _surface = 76; // [m^2]
    private double _density = 1.204; // [kg / m^3]
    private double _specificHeatCapacity = 1005; // [J / (kg K)]
    private double _thermalTransmittance = 0.5; // [W/(m^2 K)]


    // heating in [W]
    // dt is tick time in [s]
    public void ComputeTick(double heating, double dt)
    {
        var heatLost = _surface * _thermalTransmittance * (Temperature - _environmentTemperature);
        Temperature += (heating - heatLost) * dt / (_specificHeatCapacity * _volume * _density);
    }
}