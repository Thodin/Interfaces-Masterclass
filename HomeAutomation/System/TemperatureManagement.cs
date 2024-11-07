using HomeAutomation.Actuators;
using HomeAutomation.Controllers;
using HomeAutomation.Sensors;

namespace HomeAutomation.System;

public class TemperatureManagement(
    ITemperatureProvider temperatureProvider,
    IHeatProvider heatProvider,
    ITemperatureController temperatureController)
{
    private readonly ITemperatureProvider _tempProvider = temperatureProvider;
    private readonly IHeatProvider _heatProvider = heatProvider;
    private readonly ITemperatureController _tempController = temperatureController;

    public void Run()
    {
        double targetTemperature = _tempController.GetTargetTemperature();
        double curTemperature = _tempProvider.GetCurTemperature();

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