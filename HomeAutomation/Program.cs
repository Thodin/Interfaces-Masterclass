using System.Globalization;
using HomeAutomation.Actuators;
using HomeAutomation.Controllers;
using HomeAutomation.Sensors;
using HomeAutomation.Simulation;
using HomeAutomation.System;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

double simulationInterval = 10; // [s]
int numSimulationSteps = 70;

var room = new Room(20.0);
var tempSensor = new PerfectTemperatureSensor(room);
var heater = new SimpleHeater();
var tempController = new SimpleTemperatureController(24.0);

var tempManagement = new TemperatureManagement(tempSensor, heater, tempController);

for (int i = 0; i < numSimulationSteps; ++i)
{
    tempManagement.Run();

    // Simulate the room.
    room.ComputeTick(heater.GetCurPower(), simulationInterval);
}
