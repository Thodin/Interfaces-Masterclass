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

    public class MockTargetTemperatureProvider(double temp) : ITargetTemperatureProvider
    {
        private readonly double _temp = temp;

        public double GetTargetTemperature()
        {
            return _temp;
        }
    }

    public class MockHeater : IHeatProvider
    {
        private bool _isOn = false;

        public bool IsOn()
        {
            return _isOn;
        }

        public void SetHeatingPower(double wattage)
        {
            // no-op
        }

        public void TurnOff()
        {
            _isOn = false;
        }

        public void TurnOn()
        {
            _isOn = true;
        }
    }

    [Fact]
    public void HeaterTurnsOnWhenBelowTargetTemperature()
    {
        // Arrange
        var tempSensor = new MockTemperatuerSensor(20.0);
        var targetTempProvider = new MockTargetTemperatureProvider(24.0);
        var heater = new MockHeater();
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
        var targetTempProvider = new MockTargetTemperatureProvider(24.0);
        var heater = new MockHeater();
        var tempController = new TemperatureController(tempSensor, heater, targetTempProvider);

        // Act
        heater.TurnOn();
        tempController.Run();

        // Assert
        Assert.False(heater.IsOn());
    }
}