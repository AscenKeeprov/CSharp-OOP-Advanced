using System;

namespace EventImplementation
{
    public class Handler : IRenameEventHandler
    {
	public void OnDispatcherNameChange(object sender, NameChangeEventArgs eventArgs)
	{
	    Console.WriteLine($"{sender.GetType().Name}'s name changed to {eventArgs.Name}.");
	}
    }
}
