using System;

[Alias("READDB")]
public class QueryDatabaseCommand : Command
{
    private IRepository Database;

    internal override int MinRequiredParameters => 3;
    internal override int MaxAllowedParameters => 5;

    public QueryDatabaseCommand(string[] parameters, IRepository Database) : base(parameters)
    {
	this.Database = Database;
    }

    public override void Execute()
    {
	Validate(Parameters);
	string course = Parameters[1];
	string student = Parameters[2];
	string filter = null;
	string order = null;
	if (Parameters.Length >= 4)
	{
	    if (Enum.TryParse(typeof(EFilter), Parameters[3], true, out object filterValue))
		filter = filterValue.ToString();
	    else order = Parameters[3].ToUpper();
	    if (Parameters.Length == 5)
	    {
		if (Enum.TryParse(typeof(EOrder), Parameters[4], true, out object orderValue))
		    order = orderValue.ToString();
		else filter = Parameters[4].ToUpper();
	    }
	}
	if (String.IsNullOrWhiteSpace(filter)) filter = EFilter.OFF.ToString();
	if (String.IsNullOrWhiteSpace(order)) order = EOrder.ALPHABETICAL.ToString();
	Database.ReadData(course, student, filter, order);
    }
}
