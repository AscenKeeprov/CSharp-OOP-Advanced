using System;

public class CommandInterpreter
{
    private MyList<string> myList;

    public CommandInterpreter(MyList<string> myList)
    {
	this.myList = myList;
    }

    internal void BeginProcessingCommands()
    {
	string input;
	while (!(input = Console.ReadLine()).ToUpper().Equals("END"))
	{
	    try
	    {
		string[] commandArgs = input.Split();
		string command = commandArgs[0];
		switch (command.ToUpper())
		{
		    case "ADD":
			var item = commandArgs[1];
			myList.Add(item);
			break;
		    case "REMOVE":
			int index = int.Parse(commandArgs[1]);
			myList.Remove(index);
			break;
		    case "CONTAINS":
			item = commandArgs[1];
			string output = myList.Contains(item) ? "True" : "False";
			Console.WriteLine(output);
			break;
		    case "SWAP":
			int index1 = int.Parse(commandArgs[1]);
			int index2 = int.Parse(commandArgs[2]);
			myList.Swap(index1, index2);
			break;
		    case "GREATER":
			item = commandArgs[1];
			int count = myList.CountGreaterThan(item);
			Console.WriteLine(count);
			break;
		    case "MAX":
			Console.WriteLine(myList.Max());
			break;
		    case "MIN":
			Console.WriteLine(myList.Min());
			break;
		    case "PRINT":
			foreach (var entry in myList)
			{
			    Console.WriteLine(entry);
			}
			break;
		    case "SORT":
			myList.Sort();
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
