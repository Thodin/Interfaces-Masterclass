using System.Globalization;
using HomeAutomation.Actuators;
using HomeAutomation.Control;
using HomeAutomation.CurrentTemp;
using HomeAutomation.Simulation;
using HomeAutomation.TargetTemp;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

double simulationInterval = 10; // [s]
int numSimulationSteps = 200;

double initRoomTemp = 20.0;
double targetRoomTemp = 24.0;

var room = new Room(initRoomTemp);
var tempSensor = new NoisyTemperatureSensor(room);
var movingAverageFilter = new MovingAverageFilter(16);
var deadbandFilter = new DeadbandFilter(1.0);
var movingAverageDeadbandFilter = new FilterDecorator(movingAverageFilter, deadbandFilter);
var filteredTempSensor = new FilteredCurrTemperatureProvider(tempSensor, movingAverageFilter);
var heater = new SimpleHeater();
var targetTempProvider = new SimpleTargetTemperatureProvider(targetRoomTemp);

var tempController = new TemperatureController(filteredTempSensor, heater, targetTempProvider);
var wasAboveTargetTemperature = false;

for (int i = 0; i < numSimulationSteps; ++i)
{
    tempController.Run();

    // Some energy saving constraints
    if (!wasAboveTargetTemperature && tempController.IsAboveTargetTemperature())
    {
        Console.WriteLine("Changing filter to deadband.");
        deadbandFilter.SetMedianValue(targetRoomTemp);
        filteredTempSensor.SetFilter(movingAverageDeadbandFilter);
        wasAboveTargetTemperature = true;
    }
    else if (wasAboveTargetTemperature && !tempController.IsAboveTargetTemperature())
    {
        Console.WriteLine("Changing filter to moving average.");
        // movingAverageFilter.Clear();
        filteredTempSensor.SetFilter(movingAverageFilter);
        wasAboveTargetTemperature = false;
    }

    // Simulate the room.
    room.ComputeTick(heater.GetCurPower(), simulationInterval);
}
