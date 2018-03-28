using System.Collections;
using System.Collections.Generic;

public class LinkList<T> : IEnumerable<T>
{
    public Node<T> Head { get; set; }
    public Node<T> Tail { get; set; }
    public int Count;

    public IEnumerator<T> GetEnumerator()
    {
	Node<T> current = Head;
	while (current != null)
	{
	    yield return current.Value;
	    current = current.Next;
	}
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
	return GetEnumerator();
    }

    public void Add(T value)
    {
	Node<T> newNode = new Node<T>(value);
	if (Head == null) Head = newNode;
	else
	{
	    if (Head.Next == null)
	    {
		newNode.Previous = Head;
		Head.Next = newNode;
	    }
	    else
	    {
		newNode.Previous = Tail;
		Tail.Next = newNode;
	    }
	    Tail = newNode;
	}
	Count++;
    }

    public bool Remove(T value)
    {
	Node<T> current = Head;
	while (current != null)
	{
	    if (current.Value.Equals(value))
	    {
		if (current.Previous != null)
		    current.Previous.Next = current.Next;
		else Head = current.Next;
		if (current.Next != null)
		    current.Next.Previous = current.Previous;
		else Tail = current.Previous;
		Count--;
		if (Count == 1) Tail = null;
		return true;
	    }
	    current = current.Next;
	}
	return false;
    }
}
