using System;
using System.Linq;
using System.Reflection;
using Moq;
using NUnit.Framework;

namespace TirePressureMonitoringSystemTests
{
    public class AlarmTests
    {
	readonly Random rng = new Random();
	Mock<Alarm> mockAlarm;
	BindingFlags bindingFlags =
	    BindingFlags.Instance | BindingFlags.NonPublic |
	    BindingFlags.Public | BindingFlags.Static;
	FieldInfo alarmSensor => typeof(Alarm).GetFields(bindingFlags)
	    .SingleOrDefault(f => f.FieldType == typeof(Sensor));
	Mock<Sensor> mockSensor;

	[SetUp]
	public void Init()
	{
	    mockAlarm = new Mock<Alarm>();
	    mockSensor = new Mock<Sensor>();
	    alarmSensor.SetValue(mockAlarm.Object, mockSensor.Object);
	}

	[Test]
	public void AlarmNotTriggered()
	{
	    int pressureInRange = rng.Next(18, 21);
	    //mockSensor.Setup(s => s.PopNextPressurePsiValue()).Returns(pressureInRange);
	    mockSensor.SetupSequence(s => s.PopNextPressurePsiValue()).Returns(pressureInRange);
	    mockSensor.SetReturnsDefault<double>(pressureInRange);
	    mockAlarm.Object.Check();
	    Assert.False(mockAlarm.Object.AlarmOn);
	}
    }
}
