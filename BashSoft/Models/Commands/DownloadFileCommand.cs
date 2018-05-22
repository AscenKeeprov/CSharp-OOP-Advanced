[Alias("DOWNLOAD")]
public class DownloadFileCommand : Command
{
    private IFileSystemManager FSManager;

    internal override int MinRequiredParameters => 2;
    internal override int MaxAllowedParameters => 2;

    public DownloadFileCommand(string[] parameters,
	IFileSystemManager FSManager) : base(parameters)
    {
	this.FSManager = FSManager;
    }

    public override void Execute()
    {
	Validate(Parameters);
	string sourcePath = Parameters[1];
	FSManager.DownloadFile(sourcePath);
    }
}
