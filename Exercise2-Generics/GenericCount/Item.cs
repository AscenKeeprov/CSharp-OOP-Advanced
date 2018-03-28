using System;

public class Item<T> : IComparable<Item<T>>
    where T : IComparable<T>
{
    public T Value { get; }

    public Item(T value)
    {
	Value = value;
    }

    public int CompareTo(Item<T> item)
    {
	return Value.CompareTo(item.Value);
    }
}
