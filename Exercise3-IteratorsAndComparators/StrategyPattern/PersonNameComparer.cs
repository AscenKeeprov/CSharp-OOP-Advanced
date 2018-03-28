using System;
using System.Collections.Generic;

public class PersonNameComparer : IComparer<Person>
{
    public int Compare(Person person1, Person person2)
    {
	int result = person1.Name.Length.CompareTo(person2.Name.Length);
	if (result == 0)
	    result = Char.ToUpperInvariant(person1.Name[0])
		.CompareTo(Char.ToUpperInvariant(person2.Name[0]));
	return result;
    }
}
