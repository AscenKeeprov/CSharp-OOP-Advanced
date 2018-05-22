[Alias("OPEN")]
public class OpenFileCommand : Command
{
    private IInputOutputManager IOManager;
    private IFileSystemManager FSManager;

    internal override int MinRequiredParameters => 2;
    internal override int MaxAllowedParameters => 2;

    public OpenFileCommand(string[] parameters, IInputOutputManager IOManager,
	IFileSystemManager FSManager) : base(parameters)
    {
	this.IOManager = IOManager;
	this.FSManager = FSManager;
    }

    public override void Execute()
    {
	Validate(Parameters);
	string path = Parameters[1];
	FileReadingFeedback fileReadingFeedback = new FileReadingFeedback();
	string[] fileContents = FSManager.ReadFile(path);
	IOManager.OutputLine(typeof(Feedback), fileReadingFeedback.BeginMessage);
	if (fileContents != null)
	    IOManager.OutputLine(typeof(Feedback), fileReadingFeedback.ProgressMessage);
	if (fileContents.Length > 0)
	{
	    IOManager.OutputLine(typeof(Feedback), fileReadingFeedback.ResultMessage);
	    IOManager.DisplayFileContents(fileContents);
	}
	else IOManager.OutputLine(typeof(Feedback), fileReadingFeedback.EndMessage);
    }
}
