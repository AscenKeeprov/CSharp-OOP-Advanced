using System;
using System.Collections.Generic;
using System.Linq;

public class ListIterator : IIterator
{
    private List<string> items;
    private int currentIndex;

    public IReadOnlyCollection<string> Items
    {
	get { return items; }
	private set
	{
	    if (value == null)
		throw new ArgumentNullException("Collection cannot be empty!");
	    items = value.ToList();
	}
    }

    public ListIterator(params string[] items)
    {
	Items = new List<string>(items);
    }

    public bool HasNext()
    {
	if (items.Count == 0) return false;
	if (currentIndex < items.Count - 1) return true;
	return false;
    }

    public bool Move()
    {
	if (!HasNext()) return false;
	currentIndex++;
	return true;
    }

    public void Print()
    {
	if (items.Count == 0)
	    throw new InvalidOperationException("Invalid Operation!");
	Console.WriteLine(items[currentIndex]);
    }
}
