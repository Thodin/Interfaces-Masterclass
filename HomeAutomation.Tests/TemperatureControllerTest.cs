using HomeAutomation.Actuators;
using HomeAutomation.Control;
using HomeAutomation.CurrentTemp;
using HomeAutomation.TargetTemp;

namespace HomeAutomation.Tests;

// Setup xUnit for testing: https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test

public class TemperatureControllerTest
{
    public class MockTemperatuerSensor(double temp) : ICurrTemperatureProvider
    {
        private readonly double _temp = temp;

        public double GetCurrTemperature()
        {
            return _temp;
        }
    }

    [Fact]
    public void HeaterTurnsOnWhenBelowTargetTemperature()
    {
        // Arrange
        var tempSensor = new MockTemperatuerSensor(20.0);
        var targetTempProvider = new SimpleTargetTemperatureProvider(24.0);
        var heater = new SimpleHeater();
        var tempController = new TemperatureController(tempSensor, heater, targetTempProvider);

        // Act
        heater.TurnOff();
        tempController.Run();

        // Assert
        Assert.True(heater.IsOn());
    }

    [Fact]
    public void HeaterTurnsOffWhenAboveTargetTemperature()
    {
        // Arrange
        var tempSensor = new MockTemperatuerSensor(28.0);
        var targetTempProvider = new SimpleTargetTemperatureProvider(24.0);
        var heater = new SimpleHeater();
        var tempController = new TemperatureController(tempSensor, heater, targetTempProvider);

        // Act
        heater.TurnOn();
        tempController.Run();

        // Assert
        Assert.False(heater.IsOn());
    }
}