using HomeAutomation.Actuators;
using HomeAutomation.CurrentTemp;
using HomeAutomation.TargetTemp;

namespace HomeAutomation.Control;

public class TemperatureController(
    ICurrTemperatureProvider currTempProvider,
    IHeatProvider heatProvider,
    ITargetTemperatureProvider targetTempProvider)
{
    private readonly ICurrTemperatureProvider _currTempProvider = currTempProvider;
    private readonly IHeatProvider _heatProvider = heatProvider;
    private readonly ITargetTemperatureProvider _targetTempProvider = targetTempProvider;

    public void Run()
    {
        double targetTemperature = _targetTempProvider.GetTargetTemperature();
        double curTemperature = _currTempProvider.GetCurrTemperature();

        Console.WriteLine($"Current temp: {curTemperature:F2}°C, target temp: {targetTemperature:F2}°C");

        if (curTemperature < targetTemperature)
        {
            if (!_heatProvider.IsOn())
            {
                Console.WriteLine("Turning heater on.");
                _heatProvider.TurnOn();
                _heatProvider.SetHeatingPower(1000);
            }
        }
        else
        {
            if (_heatProvider.IsOn())
            {
                Console.WriteLine("Turning heater off.");
                _heatProvider.TurnOff();
            }
        }
    }
}