using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace IntDatabaseTests
{
    [TestFixture]
    public class DatabaseTests
    {
	Database<int> database;
	BindingFlags BindingFlags =>
		BindingFlags.Instance | BindingFlags.NonPublic |
		BindingFlags.Static | BindingFlags.Public;
	FieldInfo[] databaseFields => database.GetType().GetFields(BindingFlags);
	FieldInfo defaultDatabaseCapacity => databaseFields.FirstOrDefault(f
	    => f.IsPrivate && f.IsLiteral && f.IsStatic && !f.IsInitOnly
	    && f.FieldType == typeof(Int32) && f.Name.ToUpper().Contains("CAPACITY"));
	FieldInfo databaseRecords => databaseFields.FirstOrDefault(f
	    => f.IsPrivate && f.FieldType == typeof(Int32[])
	    && f.Name.ToUpper().Contains("RECORDS"));
	FieldInfo databaseRecordsCount => databaseFields.FirstOrDefault(f
	    => f.FieldType == typeof(Int32) && f.Name.ToUpper().Contains("COUNT"));
	FieldInfo currentDatabaseIndex => databaseFields.FirstOrDefault(f
	    => f.FieldType == typeof(Int32) && f.Name.ToUpper().Contains("INDEX"));

	[SetUp]
	public void Init()
	{
	    database = new Database<int>(new int[] { });
	}

	static object[] ValidInputCases =
	    {
	    new int[] { },
	    new int[16],
	    new List<int>(),
	    new int[] { Int32.MinValue, Int32.MaxValue },
	    new List<int> { Int32.MinValue, Int32.MaxValue }
	    };

	[Test, TestCaseSource("ValidInputCases")]
	public void DatabaseConstructorValidInput(IEnumerable<int> records)
	{
	    Assert.DoesNotThrow(() => new Database<int>(records));
	}

	[Test]
	public void DatabaseRecordsCollectionValidType()
	{
	    Assert.That(databaseRecords, Is.Not.Null, Exceptions.InvalidCollectionType);
	}

	[Test]
	public void DatabaseValidCapacity()
	{
	    int[] databaseContents = (int[])databaseRecords.GetValue(database);
	    int actualDatabaseCapacity = databaseContents.Length;
	    int expectedDatabaseCapacity = (int)defaultDatabaseCapacity.GetValue(database);
	    Assert.That(actualDatabaseCapacity == expectedDatabaseCapacity,
		Exceptions.InvalidDatabaseCapacity);
	}

	static object[] InvalidParametersCountCases = { new int[17] };

	[Test, TestCaseSource("InvalidParametersCountCases")]
	public void DatabaseConstructorInvalidParametersCount(IEnumerable<int> records)
	{
	    Assert.Throws<InvalidOperationException>(() => new Database<int>(records));
	}

	static object[] InvalidInputCases =
	    {
	    new List<float>(),
	    new string[] { "String 1", "String 2" },
	    new long[] { long.MinValue, long.MaxValue },
	    new double[] { Double.MinValue, Double.MaxValue }
	    };

	[Test, TestCaseSource("InvalidInputCases")]
	public void DatabaseConstructorInvalidInputType<T>(IEnumerable<T> records)
	{
	    Assert.Throws<InvalidCastException>(() => new Database<T>(records));
	}

	[Test]
	public void DatabaseAddRecordWhileEmpty()
	{
	    int newRecord = Int32.MaxValue;
	    database.Add(newRecord);
	    int[] databaseContents = (int[])databaseRecords.GetValue(database);
	    Assert.That(databaseContents[0] == newRecord);
	}

	[Test]
	public void DatabaseAddRecordWhileFull()
	{
	    int maxDatabaseCapacity = (int)defaultDatabaseCapacity.GetValue(database);
	    databaseRecordsCount.SetValue(database, maxDatabaseCapacity);
	    Assert.Throws<InvalidOperationException>(() => database.Add(1));
	}

	[Test]
	public void DatabaseRemoveRecordWhileNotEmpty()
	{
	    database.Add(Int32.MinValue);
	    int lastIndexBeforeRemove = (int)currentDatabaseIndex.GetValue(database);
	    database.Remove();
	    int lastIndexAfterRemove = (int)currentDatabaseIndex.GetValue(database);
	    Assert.That(lastIndexAfterRemove == lastIndexBeforeRemove - 1);
	}

	[Test]
	public void DatabaseRemoveRecordWhileEmpty()
	{
	    Assert.Throws<InvalidOperationException>(() => database.Remove());
	}

	[Test]
	public void DatabaseFetchValidReturnType()
	{
	    Type returnType = database.Fetch().GetType().BaseType;
	    Assert.That(returnType == typeof(Array));
	}

	[Test]
	public void DatabaseFetchValidReturnCount()
	{
	    IEnumerable<int> values = new int[] { 1, 2, 3, 4 };
	    database = new Database<int>(values);
	    int fetchedValuesCount = database.Fetch().Count();
	    Assert.That(fetchedValuesCount == values.Count());
	}
    }
}
