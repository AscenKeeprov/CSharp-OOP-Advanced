using System;

public class Pet : IComparable<Pet>
{
    public string Name { get; }
    public int Age { get; }
    public string Kind { get; }

    public Pet(string name, int age, string kind)
    {
	Name = name;
	Age = age;
	Kind = kind;
    }

    public int CompareTo(Pet other)
    {
	int result = Kind.CompareTo(other.Kind);
	if (result == 0)
	    result = Name.CompareTo(other.Name);
	if (result == 0)
	    result = Age.CompareTo(other.Age);
	return result;
    }

    public override string ToString()
    {
	return $"{Name} {Age} {Kind}";
    }
}
