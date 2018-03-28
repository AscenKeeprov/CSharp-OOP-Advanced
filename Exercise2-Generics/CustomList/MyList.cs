using System;
using System.Collections;
using System.Collections.Generic;

public class MyList<T> : IEnumerable<T>, IMyList<T>
    where T : IComparable<T>
{
    int Count => Items.Length;
    T[] Items;

    public IEnumerator<T> GetEnumerator()
    {
	return ((IEnumerable<T>)Items).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
	return GetEnumerator();
    }

    public MyList()
    {
	Items = new T[0];
    }

    public void Add(T item)
    {
	T[] newItems = new T[Count + 1];
	Array.Copy(Items, newItems, Count);
	newItems[Count] = item;
	Items = newItems;
    }

    public T Remove(int index)
    {
	if (Count == 0) throw new ListEmptyException();
	try
	{
	    T removedItem = Items[index];
	    T[] newItems = new T[Count - 1];
	    for (int i = 0; i < index; i++)
	    {
		newItems[i] = Items[i];
	    }
	    for (int i = index + 1; i < Count; i++)
	    {
		newItems[i - 1] = Items[i];
	    }
	    Items = newItems;
	    return removedItem;
	}
	catch (IndexOutOfRangeException)
	{
	    throw new InvalidIndexException();
	}
    }

    public bool Contains(T item)
    {
	for (int i = 0; i < Count; i++)
	{
	    if (Items[i].Equals(item)) return true;
	}
	return false;
    }

    public void Swap(int index1, int index2)
    {
	if (index1 < 0 || index1 >= Count ||
	    index2 < 0 || index2 >= Count)
	    throw new InvalidIndexException();
	T buffer = Items[index1];
	Items[index1] = Items[index2];
	Items[index2] = buffer;
    }

    public int CountGreaterThan(T item)
    {
	int counter = 0;
	for (int i = 0; i < Count; i++)
	{
	    if (Items[i].CompareTo(item) == 1)
		counter++;
	}
	return counter;
    }

    public T Max()
    {
	if (Count == 0) throw new ListEmptyException();
	T maxItem = Items[0];
	for (int i = 1; i < Count; i++)
	{
	    if (Items[i].CompareTo(maxItem) == 1)
		maxItem = Items[i];
	}
	return maxItem;
    }

    public T Min()
    {
	if (Count == 0) throw new ListEmptyException();
	T minItem = Items[0];
	for (int i = 1; i < Count; i++)
	{
	    if (Items[i].CompareTo(minItem) == -1)
		minItem = Items[i];
	}
	return minItem;
    }

    public void Sort()
    {
	//if (Count == 0) throw new ListEmptyException();
	Array.Sort(Items);
    }

    public override string ToString()
    {
	return String.Join(Environment.NewLine, Items);
    }
}
