using System;

public class Engine : IEngine
{
    private IReader reader;
    private IWriter writer;
    private IGameController gameController;

    public Engine(IReader reader, IWriter writer, IGameController gameController)
    {
	this.reader = reader;
	this.writer = writer;
	this.gameController = gameController;
    }

    public void Run()
    {
	string input;
	while (!(input = reader.ReadLine()).Equals("Enough! Pull back!"))
	{
	    try
	    {
		gameController.GiveInputToGameController(input);
	    }
	    catch (Exception exception)
	    {
		writer.WriteLine(exception.Message);
	    }
	}
	string result = gameController.RequestResult();
	writer.WriteLine(result);
    }
}
