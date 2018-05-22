using System;
using System.Threading;

[Alias("EXIT")]
public class ExitCommand : Command
{
    private IInputOutputManager IOManager;
    private IUserInterface UserInterface;

    internal override int MaxAllowedParameters => 1;

    public ExitCommand(string[] parameters, IInputOutputManager IOManager,
	IUserInterface UserInterface) : base(parameters)
    {
	this.IOManager = IOManager;
	this.UserInterface = UserInterface;
    }

    public override void Execute()
    {
	Validate(Parameters);
	IOManager.OutputLine(typeof(Feedback), new ProgramExitingFeedback().Message);
	IOManager.OutputLine();
	Thread.Sleep(4000);
	UserInterface.Unload();
	Environment.Exit(0);
    }
}
