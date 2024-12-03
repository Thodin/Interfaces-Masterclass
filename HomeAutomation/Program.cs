using System.Globalization;
using HomeAutomation.Actuators;
using HomeAutomation.Control;
using HomeAutomation.CurrentTemp;
using HomeAutomation.Logging;
using HomeAutomation.Simulation;
using HomeAutomation.TargetTemp;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

double simulationInterval = 10; // [s]
int numSimulationSteps = 70;

var room = new Room(20.0);
var tempSensor = new PerfectTemperatureSensor(room);
var heater = new SimpleHeater();
var targetTempProvider = new SimpleTargetTemperatureProvider(24.0);

var consoleLogger = new ConsoleLogger();
var fileLogger = new FileLogger("log.txt");

var tempController = new TemperatureController(tempSensor, heater, targetTempProvider);

tempController.SetLogger(fileLogger);

for (int i = 0; i < numSimulationSteps; ++i)
{
    tempController.Run();

    // Simulate the room.
    room.ComputeTick(heater.GetCurPower(), simulationInterval);
}
