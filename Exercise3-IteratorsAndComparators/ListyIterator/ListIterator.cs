using System;
using System.Collections;
using System.Collections.Generic;

public class ListIterator<T> : IListIterator<T>
{
    private List<T> list;
    private int index;

    public IEnumerator<T> GetEnumerator()
    {
	for (int i = 0; i < list.Count; i++)
	    yield return list[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
	return GetEnumerator();
    }

    public ListIterator(IReadOnlyCollection<T> list)
    {
	this.list = new List<T>(list);
	index = 0;
    }

    public bool Move()
    {
	if (HasNext())
	{
	    index++;
	    return true;
	}
	return false;
    }

    public bool HasNext()
    {
	return index < list.Count - 1;
    }

    public T Print()
    {
	if (list.Count == 0)
	    throw new InvalidOperationException("Invalid Operation!");
	return list[index];
    }

    public string PrintAll()
    {
	if (list.Count == 0)
	    throw new InvalidOperationException("Invalid Operation!");
	string output = String.Empty;
	foreach (var item in list)
	    output += item + " ";
	return output.TrimEnd();
    }
}
