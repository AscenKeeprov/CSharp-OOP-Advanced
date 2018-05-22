using System;

public class Person : IComparable<Person>
{
    private long id;

    public long Id
    {
	get { return id; }
	private set
	{
	    if (value < 0)
		throw new ArgumentException("Id cannot be negative!");
	    id = value;
	}
    }

    private string username;

    public string Username
    {
	get { return username; }
	private set
	{
	    if (String.IsNullOrWhiteSpace(value))
		throw new ArgumentException("Username cannot be empty!");
	    username = value;
	}
    }

    public Person(long id, string username)
    {
	Id = id;
	Username = username;
    }

    public int CompareTo(Person other)
    {
	int result = Id.CompareTo(other.Id);
	return result;
    }
}
