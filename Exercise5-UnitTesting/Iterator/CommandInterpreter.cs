using System;
using System.Linq;

public class CommandInterpreter
{
    private IIterator iterator;

    public void BeginProcessingCommands()
    {
	string input;
	while (!(input = Console.ReadLine()).Equals("END"))
	{
	    try
	    {
		string[] commandArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		string command = commandArgs[0];
		switch (command.ToUpper())
		{
		    case "CREATE":
			if (commandArgs.Length > 1)
			{
			    string[] values = commandArgs.Skip(1).ToArray();
			    iterator = new ListIterator(values);
			}
			else iterator = new ListIterator();
			break;
		    case "HASNEXT":
			Console.WriteLine(iterator.HasNext());
			break;
		    case "MOVE":
			Console.WriteLine(iterator.Move());
			break;
		    case "PRINT":
			iterator.Print();
			break;
		}
	    }
	    catch (Exception exception)
	    {
		Console.WriteLine(exception.Message);
	    }
	}
    }
}
