using System.Globalization;
using HomeAutomation.Actuators;
using HomeAutomation.Control;
using HomeAutomation.CurrentTemp;
using HomeAutomation.Simulation;
using HomeAutomation.TargetTemp;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

double simulationInterval = 10; // [s]
int numSimulationSteps = 70;

var room = new Room(20.0);
var tempSensor = new PerfectTemperatureSensor(room);
var heater = new SimpleHeater();
var tempController = new SimpleTargetTemperatureProvider(24.0);

var tempManagement = new TemperatureController(tempSensor, heater, tempController);

for (int i = 0; i < numSimulationSteps; ++i)
{
    tempManagement.Run();

    // Simulate the room.
    room.ComputeTick(heater.GetCurPower(), simulationInterval);
}
