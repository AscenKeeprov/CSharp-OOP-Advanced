namespace TheLastArmy.App
{
    public class LastArmyMain
    {
	public static void Main()
	{
	    IReader consoleReader = new ConsoleReader();
	    IWriter consoleWriter = new ConsoleWriter();
	    GameController gameController = new GameController();
	    Engine engine = new Engine(consoleReader, consoleWriter, gameController);
	    engine.Run();
	}
    }
}
