using System;

namespace EventImplementation
{
    public class StartUp
    {
	public static void Main()
	{
	    IRenameEventRaiser dispatcher = new Dispatcher("Haven Onamei");
	    IRenameEventHandler handler = new Handler();
	    dispatcher.NameChange += handler.OnDispatcherNameChange;
	    string input;
	    while (!(input = Console.ReadLine()).Equals("End"))
	    {
		dispatcher.Name = input;
	    }
	}
    }
}
