using System;

class Engine : IRunnable
{
    private IRepository repository;
    private IUnitFactory unitFactory;
    private CommandInterpreter commandInterpreter;

    public Engine(IRepository repository, IUnitFactory unitFactory, CommandInterpreter commandInterpreter)
    {
	this.repository = repository;
	this.unitFactory = unitFactory;
	this.commandInterpreter = commandInterpreter;
    }

    public void Run()
    {
	while (true)
	{
	    try
	    {
		string[] input = Console.ReadLine().Split();
		string output = commandInterpreter.InterpretCommand(input);
		Console.WriteLine(output);
	    }
	    catch (Exception exception)
	    {
		Console.WriteLine(exception.Message);
	    }
	}
    }
}
