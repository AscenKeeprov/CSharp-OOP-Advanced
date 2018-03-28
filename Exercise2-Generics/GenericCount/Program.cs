using System;
using System.Collections.Generic;

namespace GenericCount
{
    class Program
    {
	static void Main()
	{
	    List<Item<double>> items = new List<Item<double>>();
	    int itemsCount = int.Parse(Console.ReadLine());
	    for (int i = 1; i <= itemsCount; i++)
	    {
		double value = double.Parse(Console.ReadLine());
		Item<double> item = new Item<double>(value);
		items.Add(item);
	    }
	    double compareToValue = double.Parse(Console.ReadLine());
	    Item<double> compareToItem = new Item<double>(compareToValue);
	    int greatersCount = CountGreaters(items, compareToItem);
	    Console.WriteLine(greatersCount);
	}

	static int CountGreaters<T>(List<T> items, T compareToItem)
	    where T : IComparable<T>
	{
	    int greatersCount = 0;
	    foreach (var item in items)
	    {
		if (item.CompareTo(compareToItem) == 1)
		    greatersCount++;
	    }
	    return greatersCount;
	}
    }
}
