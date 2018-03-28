using System.Collections.Generic;

public class PersonAgeComparer : IComparer<Person>
{
    public int Compare(Person person1, Person person2)
    {
	return person1.Age.CompareTo(person2.Age);
    }
}
