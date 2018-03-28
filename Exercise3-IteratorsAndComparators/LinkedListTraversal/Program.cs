using System;

namespace LinkedListTraversal
{
    class Program
    {
	static void Main()
	{
	    LinkList<int> list = new LinkList<int>();
	    int commandsCount = int.Parse(Console.ReadLine());
	    for (int c = 1; c <= commandsCount; c++)
	    {
		string[] commandArgs = Console.ReadLine().Split();
		string command = commandArgs[0];
		int number = int.Parse(commandArgs[1]);
		switch (command.ToUpper())
		{
		    case "ADD":
			list.Add(number);
			break;
		    case "REMOVE":
			list.Remove(number);
			break;
		}
	    }
	    Console.WriteLine(list.Count);
	    if (list.Count > 0)
	    {
		foreach (int number in list)
		    Console.Write($"{number} ");
		Console.WriteLine();
	    }
	}
    }
}
