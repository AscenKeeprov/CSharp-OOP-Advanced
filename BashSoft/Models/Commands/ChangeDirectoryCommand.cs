[Alias("GOTODIR")]
public class ChangeDirectoryCommand : Command
{
    private IFileSystemManager FSManager;

    internal override int MinRequiredParameters => 2;
    internal override int MaxAllowedParameters => 2;

    public ChangeDirectoryCommand(string[] parameters, IFileSystemManager FSManager) : base(parameters)
    {
	this.FSManager = FSManager;
    }

    public override void Execute()
    {
	Validate(Parameters);
	string destinationPath = Parameters[1];
	FSManager.ChangeDirectory(destinationPath);
    }
}
