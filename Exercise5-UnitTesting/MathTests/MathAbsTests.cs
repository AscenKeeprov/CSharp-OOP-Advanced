using System;
using Moq;
using NUnit.Framework;

namespace MathTests
{
    [TestFixture]
    public class MathAbsTests
    {
	private const double offset = 0.00000000000000022;

	Mock<CustomMath> mockMath;
	readonly Random rng = new Random();

	[Test]
	public void AbsoluteValueOfAPositiveNumber()
	{
	    double number = (rng.NextDouble() + offset) * double.MaxValue;
	    mockMath = new Mock<CustomMath>(number);
	    double expectedResult = Math.Abs(number);
	    double actualResult = mockMath.Object.Abs;
	    Assert.AreEqual(expectedResult, actualResult);
	}

	[Test]
	public void AbsoluteValueOfANegativeNumber()
	{
	    double number = (rng.NextDouble() + offset) * double.MinValue;
	    mockMath = new Mock<CustomMath>(number);
	    double expectedResult = Math.Abs(number);
	    double actualResult = mockMath.Object.Abs;
	    Assert.AreEqual(expectedResult, actualResult);
	}
    }
}
