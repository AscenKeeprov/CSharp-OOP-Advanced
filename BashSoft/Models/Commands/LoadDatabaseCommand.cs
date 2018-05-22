[Alias("LOADDB")]
public class LoadDatabaseCommand : Command
{
    private IRepository Database;

    internal override int MinRequiredParameters => 2;
    internal override int MaxAllowedParameters => 2;

    public LoadDatabaseCommand(string[] parameters, IRepository Database) : base(parameters)
    {
	this.Database = Database;
    }

    public override void Execute()
    {
	Validate(Parameters);
	string path = Parameters[1];
	Database.LoadData(path);
    }
}
