public class CreateTask : Task
{
    [Inject]
    private IArmoury armoury;
    [Inject]
    private IBlacksmith blacksmith;

    protected IArmoury Armoury
    {
	get { return armoury; }
	set { armoury = value; }
    }

    protected IBlacksmith Blacksmith
    {
	get { return blacksmith; }
	set { blacksmith = value; }
    }

    public CreateTask(string[] parameters, IArmoury armoury, IBlacksmith blacksmith)
	: base(parameters)
    {
	Armoury = armoury;
	Blacksmith = blacksmith;
    }

    public override void Execute()
    {
	string weaponRarity = Parameters[0].Split()[0];
	string weaponType = Parameters[0].Split()[1];
	string weaponName = Parameters[1];
	string[] weaponInfo = new string[]
	{
	    weaponRarity,
	    weaponType,
	    weaponName
	};
	IWeapon weapon = Blacksmith.Forge(weaponInfo);
	Armoury.StoreWeapon(weapon);
    }
}
