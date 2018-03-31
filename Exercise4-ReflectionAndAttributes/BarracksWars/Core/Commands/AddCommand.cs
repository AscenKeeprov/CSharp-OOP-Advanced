public class AddCommand : Command
{
    [Inject("Repository")]
    private IRepository repository;
    [Inject("UnitFactory")]
    private IUnitFactory unitFactory;

    public AddCommand(string[] data) : base(data) { }

    public override string Execute()
    {
	string unitType = Data[0];
	IUnit unitToAdd = unitFactory.CreateUnit(unitType);
	repository.AddUnit(unitToAdd);
	string output = unitType + " added!";
	return output;
    }
}
