using HomeAutomation.Actuators;
using HomeAutomation.Controllers;
using HomeAutomation.Sensors;
using HomeAutomation.System;

double simulationInterval = 10; // [s]
int numSimulationSteps = 100;

var room = new Room(20.0);
var tempSensor = new DummyTemperatureSensor(room);
var heater = new DummyHeater();
var tempController = new DummyTemperatureController();

var tempManagement = new TemperatureManagement(tempSensor, heater, tempController);

for (int i = 0; i < numSimulationSteps; ++i)
{
    tempManagement.Run();

    // Simulate the room.
    room.ComputeTick(heater.GetCurPower(), simulationInterval);
}
