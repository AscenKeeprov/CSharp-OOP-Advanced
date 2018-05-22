[Alias("LISTDIR")]
public class TraverseDirectoryCommand : Command
{
    private IFileSystemManager FSManager;

    internal override int MaxAllowedParameters => 2;

    public TraverseDirectoryCommand(string[] parameters,
	IFileSystemManager FSManager) : base(parameters)
    {
	this.FSManager = FSManager;
    }

    public override void Execute()
    {
	Validate(Parameters);
	if (Parameters.Length == 1) FSManager.TraverseDirectory(0);
	else if (!int.TryParse(Parameters[1], out int depth))
	    throw new InvalidCommandParameterException("Directory traversal depth");
	else FSManager.TraverseDirectory(depth);
    }
}
