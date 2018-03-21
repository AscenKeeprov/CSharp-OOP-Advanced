using System;

namespace LibLogging
{
    class StartUp
    {
	static void Main()
	{
	    Controller controller = new Controller();
	    try
	    {
		controller.InitializeAppenders();
		controller.InitializeLogger();
		controller.Logger.BeginProcessingMessages();
		controller.DisplayAppendersInfo();
	    }
	    catch (Exception exception)
	    {
		Console.WriteLine(exception.Message);
	    }
	}
    }
}
