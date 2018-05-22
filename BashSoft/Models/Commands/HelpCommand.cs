using System;

[Alias("HELP")]
public class HelpCommand : Command
{
    private const string HelpFileLocation = "resources/help.txt";

    private IFileSystemManager FSManager;
    private IInputOutputManager IOManager;

    internal override int MaxAllowedParameters => 1;

    public HelpCommand(string[] parameters, IFileSystemManager FSManager,
	IInputOutputManager IOManager) : base(parameters)
    {
	this.FSManager = FSManager;
	this.IOManager = IOManager;
    }

    public override void Execute()
    {
	Validate(Parameters);
	Console.Clear();
	string[] helpText = FSManager.ReadFile(HelpFileLocation);
	foreach (string line in helpText)
	{
	    IOManager.OutputLine(typeof(String), line);
	}
    }
}
