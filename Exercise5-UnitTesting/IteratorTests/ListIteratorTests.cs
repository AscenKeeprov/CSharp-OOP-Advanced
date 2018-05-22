using System;
using System.IO;
using NUnit.Framework;

namespace IteratorTests
{
    [TestFixture]
    public class ListIteratorTests
    {
	IIterator iterator;

	private static readonly object[] ValidInputCases =
	    {
	    new string[] { },
	    new string[] { "Value" }
	    };

	[Test, TestCaseSource("ValidInputCases")]
	public void IteratorConstructorValidInput(string[] values)
	{
	    Assert.DoesNotThrow(() => new ListIterator(values));
	}

	[Test]
	public void IteratorConstructorInvalidInput()
	{
	    Assert.Throws<ArgumentNullException>(() => new ListIterator(null));
	}

	[Test]
	public void IteratorHasNextFunctionWhenThereIsNext()
	{
	    string[] values = new string[] { "Value 1", "Value 2" };
	    iterator = new ListIterator(values);
	    Assert.True(iterator.HasNext());
	}

	[Test, TestCaseSource("ValidInputCases")]
	public void IteratorHasNextFunctionWhenThereIsNoNext(string[] values)
	{
	    iterator = new ListIterator(values);
	    Assert.False(iterator.HasNext());
	}

	[Test]
	public void IteratorMoveFunctionWhenHasNext()
	{
	    string[] values = new string[] { "Value 1", "Value 2" };
	    iterator = new ListIterator(values);
	    Assert.True(iterator.Move());
	}

	[Test, TestCaseSource("ValidInputCases")]
	public void IteratorMoveFunctionWhenHasNoNext(string[] values)
	{
	    iterator = new ListIterator(values);
	    Assert.False(iterator.Move());
	}

	[Test]
	public void IteratorPrintFunctionWithElements()
	{
	    string expectedValue = "expectedValue";
	    iterator = new ListIterator(new string[] { expectedValue });
	    using (StringWriter sw = new StringWriter())
	    {
		Console.SetOut(sw);
		iterator.Print();
		string actualValue = sw.ToString().TrimEnd();
		Assert.AreEqual(expectedValue, actualValue);
	    }
	}

	[Test]
	public void IteratorPrintFunctionWithoutElements()
	{
	    iterator = new ListIterator();
	    Assert.Throws<InvalidOperationException>(() => iterator.Print());
	}
    }
}
