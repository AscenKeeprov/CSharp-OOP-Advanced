namespace CustomList
{
    class Program
    {
	static void Main()
	{
	    MyList<string> myList = new MyList<string>();
	    CommandInterpreter commandInterpreter = new CommandInterpreter(myList);
	    commandInterpreter.BeginProcessingCommands();
	}
    }
}
