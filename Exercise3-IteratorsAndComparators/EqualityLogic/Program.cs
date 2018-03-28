using System;
using System.Collections.Generic;

namespace EqualityLogic
{
    class Program
    {
        static void Main()
        {
	    SortedSet<Person> peopleSorted = new SortedSet<Person>();
	    HashSet<Person> peopleHashed = new HashSet<Person>();
	    int peopleCount = int.Parse(Console.ReadLine());
	    for (int c = 1; c <= peopleCount; c++)
	    {
		string[] personInfo = Console.ReadLine().Split();
		string name = personInfo[0];
		int age = int.Parse(personInfo[1]);
		Person person = new Person(name, age);
		peopleSorted.Add(person);
		peopleHashed.Add(person);
	    }
	    Console.WriteLine(peopleSorted.Count);
	    Console.WriteLine(peopleHashed.Count);
	}
    }
}
