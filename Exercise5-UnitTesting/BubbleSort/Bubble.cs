using System;
using System.Collections.Generic;
using System.Linq;

public class Bubble : ISorter
{
    int[] items;

    public Bubble(ICollection<int> items)
    {
	if (items == null)
	    throw new ArgumentException("Cannot sort empty collections!");
	this.items = items.ToArray();
    }

    public void Sort()
    {
	if (items.Length == 0)
	    throw new OperationCanceledException("Collection empty! Sorting cancelled.");
	if (items.Length == 1)
	    throw new OperationCanceledException("Single item present! Sorting cancelled.");
	if (items.All(i => i.Equals(items[0])))
	    throw new OperationCanceledException("All items equal! Sorting cancelled.");
	while (true)
	{
	    bool swapped = false;
	    for (int i = 0; i < items.Length - 1; i++)
	    {
		if (items[i] > items[i + 1])
		{
		    Swap(i, i + 1);
		    swapped = true;
		}
	    }
	    if (!swapped) break;
	}
    }

    private void Swap(int index1, int index2)
    {
	if (index1 < 0 || index2 < 0)
	    throw new ArgumentException("Index cannot be negative!");
	if (index1 == index2)
	    throw new OperationCanceledException("Indices are identical! Swap cancelled.");
	int buffer = items[index1];
	items[index1] = items[index2];
	items[index2] = buffer;
    }

    public string Print()
    {
	return String.Join(",", items);
    }
}
