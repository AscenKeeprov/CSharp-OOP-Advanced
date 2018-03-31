public class RemoveTask : Task
{
    [Inject]
    private IArmoury armoury;
    [Inject]
    private IJeweller jeweller;

    protected IArmoury Armoury
    {
	get { return armoury; }
	set { armoury = value; }
    }

    protected IJeweller Jeweller
    {
	get { return jeweller; }
	set { jeweller = value; }
    }

    public RemoveTask(string[] parameters, IArmoury armoury, IJeweller jeweller)
	: base(parameters)
    {
	Armoury = armoury;
	Jeweller = jeweller;
    }

    public override void Execute()
    {
	string weaponName = Parameters[0];
	IWeapon weapon = Armoury.TakeWeapon(weaponName);
	int socketNumber = int.Parse(Parameters[1]);
	if (weapon != null) weapon.EmptySocket(socketNumber);
    }
}
