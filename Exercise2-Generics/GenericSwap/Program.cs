using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericSwap
{
    class Program
    {
	static void Main()
	{
	    List<IItem> items = new List<IItem>();
	    int itemsCount = int.Parse(Console.ReadLine());
	    for (int i = 1; i <= itemsCount; i++)
	    {
		var value = Console.ReadLine();
		IItem item = new Item<int>(int.Parse(value));
		items.Add(item);
	    }
	    int[] swapArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
	    SwapItems(items, swapArgs);
	    foreach (IItem item in items)
	    {
		Console.WriteLine(item);
	    }
	}

	static void SwapItems<T>(List<T> items, int[] swapArgs)
	{
	    int index1 = swapArgs[0];
	    int index2 = swapArgs[1];
	    T buffer = items[index1];
	    items[index1] = items[index2];
	    items[index2] = buffer;
	}
    }
}
