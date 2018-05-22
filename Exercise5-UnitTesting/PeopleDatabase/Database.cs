using System;
using System.Collections.Generic;
using System.Linq;

public class Database<T>
{
    private const int DATABASE_CAPACITY = 16;

    private Person[] people;
    private int peopleCount;
    private int currentIndex;

    private Database()
    {
	people = new Person[DATABASE_CAPACITY];
	currentIndex = -1;
    }

    public Database(IEnumerable<T> people) : this()
    {
	ValidateInput(people);
	Array.Copy(people.ToArray(), this.people, people.Count());
	peopleCount = people.Count();
	currentIndex = people.Count() - 1;
    }

    private void ValidateInput(IEnumerable<T> records)
    {
	if (typeof(T) != typeof(Person))
	    throw new InvalidCastException("Database cannot hold records of this type!");
	if (records.Count() > DATABASE_CAPACITY)
	    throw new InvalidOperationException("Database capacity exceeded!");
    }

    public void Add(Person person)
    {
	if (peopleCount == DATABASE_CAPACITY)
	    throw new InvalidOperationException("Database full!");
	if (people.Where(p => p != null).Any(p => p.Id == person.Id))
	    throw new InvalidOperationException("A person with that id has already been registered!");
	if (people.Where(p => p != null).Any(p => p.Username == person.Username))
	    throw new InvalidOperationException("A person with that username has already been registered!");
	currentIndex++;
	people[currentIndex] = person;
	peopleCount++;
    }

    public Person FindById(long id)
    {
	if (id < 0) throw new ArgumentException("Id cannot be negative!");
	if (!people.Where(p => p != null).Any(p => p.Id == id))
	    throw new InvalidOperationException("Cannot find person with such id!");
	Person person = people.First(p => p.Id == id);
	return person;
    }

    public Person FindByUsername(string username)
    {
	if (String.IsNullOrWhiteSpace(username))
	    throw new ArgumentException("Username cannot be empty!");
	if (!people.Where(p => p != null).Any(p => p.Username == username))
	    throw new InvalidOperationException("Cannot find person with such username!");
	Person person = people.First(p => p.Username == username);
	return person;
    }

    public void Remove()
    {
	if (peopleCount == 0) throw new InvalidOperationException("Database empty!");
	people[currentIndex] = default(Person);
	peopleCount--;
	currentIndex--;
    }

    public Person[] Fetch()
    {
	if (peopleCount == 0 || peopleCount == DATABASE_CAPACITY) return people;
	Person[] peopleToFetch = new Person[peopleCount];
	Array.Copy(people, peopleToFetch, peopleCount);
	return peopleToFetch;
    }
}
