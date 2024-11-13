using HomeAutomation.Actuators;
using HomeAutomation.CurrentTemp;
using HomeAutomation.TargetTemp;

namespace HomeAutomation.Control;

public class TemperatureController(
    ICurrTemperatureProvider temperatureProvider,
    IHeatProvider heatProvider,
    ITargetTemperatureProvider temperatureController)
{
    private readonly ICurrTemperatureProvider _tempProvider = temperatureProvider;
    private readonly IHeatProvider _heatProvider = heatProvider;
    private readonly ITargetTemperatureProvider _tempController = temperatureController;

    public void Run()
    {
        double targetTemperature = _tempController.GetTargetTemperature();
        double curTemperature = _tempProvider.GetCurrTemperature();

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