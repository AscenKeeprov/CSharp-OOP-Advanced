using System;

public class Person : IComparable<Person>
{
    public string Name { get; private set; }
    public int Age { get; private set; }

    public Person(string name, int age)
    {
	Name = name;
	Age = age;
    }

    public int CompareTo(Person other)
    {
	int result = Name.CompareTo(other.Name);
	if (result == 0)
	    result = Age.CompareTo(other.Age);
	return result;
    }

    public override bool Equals(object obj)
    {
	if (obj is Person other)
	{
	    return Name.Equals(other.Name) && Age.Equals(other.Age);
	}
	return false;
    }

    public override int GetHashCode()
    {
	return Name.GetHashCode() + Age.GetHashCode();
    }
}
