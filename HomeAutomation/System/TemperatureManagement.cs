using HomeAutomation.Actuators;
using HomeAutomation.Controllers;
using HomeAutomation.Sensors;

namespace HomeAutomation.System;

public class TemperatureManagement(
    ITemperatureProvider temperatureProvider,
    IHeatProvider heatProvider,
    ITemperatureController temperatureController)
{
    private readonly ITemperatureProvider _tempSensor = temperatureProvider;
    private readonly IHeatProvider _heater = heatProvider;
    private readonly ITemperatureController _tempController = temperatureController;

    public void Run()
    {
        double targetTemperature = _tempController.GetTargetTemperature();
        double curTemperature = _tempSensor.GetCurTemperature();

        Console.WriteLine($"Current temp: {curTemperature:F2}°C, target temp: {targetTemperature:F2}°C");

        if (curTemperature < targetTemperature)
        {
            if (!_heater.IsOn())
            {
                Console.WriteLine("Turning heater on.");
                _heater.TurnOn();
                _heater.SetHeatingPower(1000);
            }
        }
        else
        {
            if (_heater.IsOn())
            {
                Console.WriteLine("Turning heater off.");
                _heater.TurnOff();
            }
        }
    }
}