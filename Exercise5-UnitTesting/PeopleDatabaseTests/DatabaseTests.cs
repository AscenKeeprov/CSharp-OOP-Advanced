using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace PeopleDatabaseTests
{
    [TestFixture]
    public class DatabaseTests
    {
	Database<Person> database;
	BindingFlags BindingFlags =>
		BindingFlags.Instance | BindingFlags.NonPublic |
		BindingFlags.Static | BindingFlags.Public;
	FieldInfo[] databaseFields => database.GetType().GetFields(BindingFlags);
	FieldInfo defaultDatabaseCapacity => databaseFields.FirstOrDefault(f
	    => f.IsPrivate && f.IsLiteral && f.IsStatic && !f.IsInitOnly
	    && f.FieldType == typeof(Int32) && f.Name.ToUpper().Contains("CAPACITY"));
	FieldInfo databaseRecords => databaseFields.FirstOrDefault(f
	    => f.IsPrivate && f.FieldType == typeof(Person[])
	    && f.Name.ToUpper().Contains("PEOPLE"));
	FieldInfo databaseRecordsCount => databaseFields.FirstOrDefault(f
	    => f.FieldType == typeof(Int32) && f.Name.ToUpper().Contains("COUNT"));
	FieldInfo currentDatabaseIndex => databaseFields.FirstOrDefault(f
	    => f.FieldType == typeof(Int32) && f.Name.ToUpper().Contains("INDEX"));

	[SetUp]
	public void Init()
	{
	    database = new Database<Person>(new Person[] { });
	}

	static object[] ValidInputCases =
	    {
	    new Person[] { },
	    new Person[16],
	    new List<Person>(),
	    new Person[]
		{
		new Person(1, "Enyo"),
		new Person(long.MaxValue, "Maks")
		},
	    new List<Person>
		{
		new Person(1, "Enyo"),
		new Person(long.MaxValue, "Maks")
		}
	    };

	[Test, TestCaseSource("ValidInputCases")]
	public void DatabaseConstructorValidInput(IEnumerable<Person> people)
	{
	    Assert.DoesNotThrow(() => new Database<Person>(people));
	}

	[Test]
	public void DatabaseRecordsCollectionValidType()
	{
	    Assert.That(databaseRecords, Is.Not.Null, Exceptions.InvalidCollectionType);
	}

	[Test]
	public void DatabaseValidCapacity()
	{
	    Person[] databaseContents = (Person[])databaseRecords.GetValue(database);
	    int actualDatabaseCapacity = databaseContents.Length;
	    int expectedDatabaseCapacity = (int)defaultDatabaseCapacity.GetValue(database);
	    Assert.That(actualDatabaseCapacity == expectedDatabaseCapacity,
		Exceptions.InvalidDatabaseCapacity);
	}

	static object[] InvalidParametersCountCases = { new Person[17] };

	[Test, TestCaseSource("InvalidParametersCountCases")]
	public void DatabaseConstructorInvalidParametersCount(IEnumerable<Person> people)
	{
	    Assert.Throws<InvalidOperationException>(() => new Database<Person>(people));
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
	    Person person = new Person(long.MaxValue, "Maks");
	    database.Add(person);
	    Person[] databaseContents = (Person[])databaseRecords.GetValue(database);
	    Assert.That(databaseContents[0], Is.EqualTo(person));
	}

	[Test]
	public void DatabaseAddRecordWhileFull()
	{
	    int maxDatabaseCapacity = (int)defaultDatabaseCapacity.GetValue(database);
	    databaseRecordsCount.SetValue(database, maxDatabaseCapacity);
	    Person person = new Person(long.MaxValue, "Maks");
	    Assert.Throws<InvalidOperationException>(() => database.Add(person));
	}

	[Test]
	public void DatabaseAddRecordsWithIdenticalIds()
	{
	    Person person1 = new Person(long.MaxValue, "Maks Senior");
	    database.Add(person1);
	    Person person2 = new Person(long.MaxValue, "Maks Junior");
	    Assert.Throws<InvalidOperationException>(() => database.Add(person2));
	}

	[Test]
	public void DatabaseAddRecordsWithIdenticalUsernames()
	{
	    Person person1 = new Person(1, "Username");
	    database.Add(person1);
	    Person person2 = new Person(2, "Username");
	    Assert.Throws<InvalidOperationException>(() => database.Add(person2));
	}

	[Test]
	public void DatabaseAddRecordsWithDifferentUsernames()
	{
	    Person person1 = new Person(1, "username");
	    database.Add(person1);
	    Person person2 = new Person(2, "Username");
	    Assert.DoesNotThrow(() => database.Add(person2));
	}

	[Test]
	public void DatabaseRemoveRecordWhileNotEmpty()
	{
	    Person person = new Person(long.MaxValue, "Maks");
	    database = new Database<Person>(new Person[] { person });
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
	[TestCase(default(long))]
	[TestCase(long.MaxValue)]
	public void DatabaseFindRecordByValidId(long id)
	{
	    IEnumerable<Person> values = new Person[]
		{
		new Person(default(long), "Nulyo"),
		new Person(long.MaxValue, "Maksyo")
		};
	    database = new Database<Person>(values);
	    Assert.DoesNotThrow(() => database.FindById(id));
	}

	[Test]
	[TestCase(long.MinValue)]
	public void DatabaseFindRecordByNegativeId(long id)
	{
	    Assert.Throws<ArgumentException>(() => database.FindById(id));
	}

	[Test]
	[TestCase(long.MaxValue)]
	public void DatabaseFindRecordByMissingId(long id)
	{
	    Person person = new Person(1, "Enyo");
	    database = new Database<Person>(new Person[] { person });
	    Assert.Throws<InvalidOperationException>(() => database.FindById(id));
	}

	[Test]
	[TestCase("John Doe")]
	[TestCase("Jane Doe")]
	public void DatabaseFindRecordByValidUsername(string username)
	{
	    IEnumerable<Person> values = new Person[]
		{
		new Person(1, "Jane Doe"),
		new Person(2, "John Doe")
		};
	    database = new Database<Person>(values);
	    Assert.DoesNotThrow(() => database.FindByUsername(username));
	}

	[Test]
	[TestCase("Username")]
	public void DatabaseFindRecordByMissingUsername(string username)
	{
	    Person person = new Person(0, "Zero");
	    database = new Database<Person>(new Person[] { person });
	    Assert.Throws<InvalidOperationException>(() => database.FindByUsername(username));
	}

	[Test]
	[TestCase("")]
	[TestCase(null)]
	[TestCase("\r\n")]
	public void DatabaseFindRecordByNullOrEmptyUsername(string username)
	{
	    Assert.Throws<ArgumentException>(() => database.FindByUsername(username));
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
	    IEnumerable<Person> values = new Person[]
		{
		new Person(1, "Enyo"),
		new Person(long.MaxValue, "Maks")
		};
	    database = new Database<Person>(values);
	    int fetchedValuesCount = database.Fetch().Count();
	    Assert.That(fetchedValuesCount == values.Count());
	}
    }
}
