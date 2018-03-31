public class RetireCommand : Command
{
    [Inject("Repository")]
    private IRepository repository;

    public RetireCommand(string[] data) : base(data) { }

    public override string Execute()
    {
	string unitType = Data[0];
	repository.RemoveUnit(unitType);
	string output = unitType + " retired!";
	return output;
    }
}
