using System;
using System.Linq;

public class CommandInterpreter
{
    private IListIterator<string> iterator;

    public void BeginProcessingCommands()
    {
	string input;
	while (!(input = Console.ReadLine()).ToUpper().Equals("END"))
	{
	    try
	    {
		string[] commandArgs = input.Split();
		var command = commandArgs[0];
		switch (command.ToUpper())
		{
		    case "CREATE":
			iterator = new ListIterator<string>(commandArgs.Skip(1).ToList());
			break;
		    case "HASNEXT":
			Console.WriteLine(iterator.HasNext());
			break;
		    case "MOVE":
			Console.WriteLine(iterator.Move());
			break;
		    case "PRINT":
			Console.WriteLine(iterator.Print());
			break;
		    case "PRINTALL":
			Console.WriteLine(iterator.PrintAll());
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
