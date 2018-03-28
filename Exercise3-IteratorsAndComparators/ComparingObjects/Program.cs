using System;
using System.Collections.Generic;

namespace ComparingObjects
{
    class Program
    {
	static void Main()
	{
	    List<Person> people = new List<Person>();
	    string input;
	    while (!(input = Console.ReadLine()).ToUpper().Equals("END"))
	    {
		string[] personInfo = input.Split();
		string name = personInfo[0];
		int age = int.Parse(personInfo[1]);
		string town = personInfo[2];
		Person person = new Person(name, age, town);
		people.Add(person);
	    }
	    int personNumber = int.Parse(Console.ReadLine());
	    Person personN = people[personNumber - 1];
	    int equalsCount = 0;
	    foreach (var person in people)
		if (person.CompareTo(personN) == 0) equalsCount++;
	    if (equalsCount < 2) Console.WriteLine("No matches");
	    else
	    {
		int nonEqualsCount = people.Count - equalsCount;
		Console.WriteLine($"{equalsCount} {nonEqualsCount} {people.Count}");
	    }
	}
    }
}
