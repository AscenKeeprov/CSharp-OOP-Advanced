[Alias("COMPARE")]
public class CompareFilesCommand : Command
{
    private ITester Tester;

    internal override int MinRequiredParameters => 3;
    internal override int MaxAllowedParameters => 3;

    public CompareFilesCommand(string[] parameters, ITester Tester) : base(parameters)
    {
	this.Tester = Tester;
    }

    public override void Execute()
    {
	Validate(Parameters);
	string file1 = Parameters[1];
	string file2 = Parameters[2];
	Tester.CompareFiles(file1, file2);
    }
}
