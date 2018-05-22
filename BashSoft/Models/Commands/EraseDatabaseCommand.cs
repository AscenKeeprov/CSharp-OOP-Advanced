[Alias("DROPDB")]
public class EraseDatabaseCommand : Command
{
    private IRepository Database;

    internal override int MaxAllowedParameters => 1;

    public EraseDatabaseCommand(string[] parameters, IRepository Database) : base(parameters)
    {
	this.Database = Database;
    }

    public override void Execute()
    {
	Validate(Parameters);
	Database.DeleteData();
    }
}
