[Alias("MAKEDIR")]
public class CreateDirectoryCommand : Command
{
    private IFileSystemManager FSManager;

    internal override int MinRequiredParameters => 2;
    internal override int MaxAllowedParameters => 2;

    public CreateDirectoryCommand(string[] parameters, IFileSystemManager FSManager) : base(parameters)
    {
	this.FSManager = FSManager;
    }

    public override void Execute()
    {
	Validate(Parameters);
	string path = Parameters[1];
	FSManager.CreateDirectory(path);
    }
}
