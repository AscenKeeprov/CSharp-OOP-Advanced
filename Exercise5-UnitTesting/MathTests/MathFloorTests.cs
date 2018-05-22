﻿using System;
using Moq;
using NUnit.Framework;

namespace MathTests
{
    [TestFixture]
    public class MathFloorTests
    {
	private const double offset = 0.00000000000000022;

	Mock<CustomMath> mockMath;
	readonly Random rng = new Random();

	[Test]
	public void FloorValueOfAPositiveDecimal()
	{
	    double number = (rng.NextDouble() + offset) * double.MaxValue;
	    mockMath = new Mock<CustomMath>(number);
	    double expectedResult = Math.Floor(number);
	    double actualResult = mockMath.Object.Floor;
	    Assert.AreEqual(expectedResult, actualResult);
	}

	[Test]
	public void FloorValueOfANegativeDecimal()
	{
	    double number = (rng.NextDouble() + offset) * double.MinValue;
	    mockMath = new Mock<CustomMath>(number);
	    double expectedResult = Math.Floor(number);
	    double actualResult = mockMath.Object.Floor;
	    Assert.AreEqual(expectedResult, actualResult);
	}
    }
}
