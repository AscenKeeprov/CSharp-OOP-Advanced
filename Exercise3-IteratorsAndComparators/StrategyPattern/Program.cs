using System;
using System.Collections.Generic;

namespace StrategyPattern
{
    class Program
    {
        static void Main()
        {
	    SortedSet<Person> peopleByName = new SortedSet<Person>(new PersonNameComparer());
	    SortedSet<Person> peopleByAge = new SortedSet<Person>(new PersonAgeComparer());
	    int peopleCount = int.Parse(Console.ReadLine());
	    for (int c = 1; c <= peopleCount; c++)
	    {
		string[] personInfo = Console.ReadLine().Split();
		string name = personInfo[0];
		int age = int.Parse(personInfo[1]);
		Person person = new Person(name, age);
		peopleByName.Add(person);
		peopleByAge.Add(person);
	    }
	    foreach (Person person in peopleByName)
		Console.WriteLine(person);
	    foreach (Person person in peopleByAge)
		Console.WriteLine(person);
	}
    }
}
