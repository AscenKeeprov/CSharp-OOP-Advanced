public class ReportCommand : Command
{
    [Inject("Repository")]
    private IRepository repository;

    public ReportCommand(string[] data) : base(data) { }

    public override string Execute()
    {
	string output = repository.Statistics;
	return output;
    }
}
