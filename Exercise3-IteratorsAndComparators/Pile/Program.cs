using System;
using System.Linq;

namespace Pile
{
    class Program
    {
	static void Main()
	{
	    IStack<int> stack = new Stack<int>();
	    string input;
	    while (!(input = Console.ReadLine()).ToUpper().Equals("END"))
	    {
		try
		{
		    string[] commandArgs = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
		    string command = commandArgs[0];
		    switch (command.ToUpper())
		    {
			case "PUSH":
			    stack.Push(commandArgs.Skip(1).Select(int.Parse).ToArray());
			    break;
			case "POP":
			    stack.Pop();
			    break;
		    }
		}
		catch (Exception exception)
		{
		    Console.WriteLine(exception.Message);
		}
	    }
	    for (int i = 1; i <= 2; i++)
	    {
		foreach (var item in stack)
		    Console.WriteLine(item);
	    }
	}
    }
}
