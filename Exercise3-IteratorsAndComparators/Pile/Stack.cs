using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Stack<T> : IStack<T>
{
    private T[] items;
    private int count => items.Length;

    public IEnumerator<T> GetEnumerator()
    {
	for (int i = items.Length - 1; i >= 0; i--)
	    yield return items[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
	return GetEnumerator();
    }

    public Stack()
    {
	items = new T[0];
    }

    public void Push(params T[] items)
    {
	int capacity = count + items.Length;
	T[] newItems = new T[capacity];
	for (int i = 0; i < count; i++)
	    newItems[i] = this.items[i];
	for (int i = 0; i < items.Length; i++)
	    newItems[i + count] = items[i];
	this.items = newItems;
    }

    public void Pop()
    {
	if (count == 0)
	    throw new InvalidOperationException("No elements");
	T[] newItems = new T[count - 1];
	for (int i = 0; i < count - 1; i++)
	    newItems[i] = items[i];
	items = newItems;
    }

    public override string ToString()
    {
	StringBuilder stackItems = new StringBuilder();
	for (int i = count - 1; i >= 0; i--)
	    stackItems.AppendLine($"{items[i]}");
	return stackItems.ToString();
    }
}
