/// <summary>Program access point.</summary>
public class Launcher
{
    public static void Main()
    {
	Server server = new Server();
	server.InitializeServices();
	IUserInterface UserInterface = (UserInterface)server
	    .GetService(typeof(IUserInterface));
	UserInterface.Load();
	IOutputWriter IOManager = (IOManager)server
	    .GetService(typeof(IInputOutputManager));
	IOManager.DisplayWelcome();
	ICommandInterpreter commandInterpreter = (CommandInterpreter)server
	    .GetService(typeof(ICommandInterpreter));
	commandInterpreter.StartProcessingCommands();
    }
}
