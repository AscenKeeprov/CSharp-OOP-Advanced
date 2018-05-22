using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace BubbleSortTests
{
    [TestFixture]
    public class BubbleTests
    {
	ISorter bubbleSorter;
	MethodInfo swapMethod => bubbleSorter.GetType()
		.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
		.First(method => method.Name.Equals("Swap"));

	[SetUp]
	public void Init()
	{
	    bubbleSorter = new Bubble(new int[] { 9, 2, -3, 53, 18 });
	}

	private static readonly object[] ConstructorValidInputCases =
	    {
	    new int[] { },
	    new List<int>() { 1 },
	    new int[] { Int32.MinValue, Int32.MaxValue },
	    };

	[Test, TestCaseSource("ConstructorValidInputCases")]
	public void SorterConstructorValidInput(ICollection<int> values)
	{
	    Assert.DoesNotThrow(() => new Bubble(values));
	}

	[Test]
	[TestCase(null)]
	public void SorterConstructorInvalidInput(ICollection<int> values)
	{
	    Assert.Throws<ArgumentException>(() => new Bubble(values));
	}

	[Test]
	public void SorterSwapFunctionalityValidIndices()
	{
	    object[] indices = new object[] { 1, 2 };
	    Assert.DoesNotThrow(() => swapMethod.Invoke(bubbleSorter, indices));
	}

	private static readonly object[] SwapFunctionalityInvalidIndicesCases =
	    {
	    new object[] { 1, 1 },
	    new object[] { -1, 0 },
	    new object[] { 0, null },
	    new object[] { "1", "O" }
	    };

	[Test, TestCaseSource("SwapFunctionalityInvalidIndicesCases")]
	public void SorterSwapFunctionalityInvalidIndices(object[] indices)
	{
	    Assert.Catch<Exception>(() => swapMethod.Invoke(bubbleSorter, indices));
	}

	[Test]
	public void SorterSortFunctionalityValidInput()
	{
	    Assert.DoesNotThrow(() => bubbleSorter.Sort());
	}

	private static readonly object[] SortFunctionalityInvalidInputCases =
	    {
	    new int[] { },
	    new int[] { 1 },
	    new int[] { 1337, 1337 }
	    };

	[Test, TestCaseSource("SortFunctionalityInvalidInputCases")]
	public void SorterSortFunctionalityInvalidInput(int[] values)
	{
	    bubbleSorter = new Bubble(values);
	    Assert.Throws<OperationCanceledException>(() => bubbleSorter.Sort());
	}

	[Test]
	public void SorterPrintFunctionality()
	{
	    string expectedResult = "9,2,-3,53,18";
	    string actualResult = bubbleSorter.Print();
	    Assert.AreEqual(expectedResult, actualResult);
	}
    }
}
