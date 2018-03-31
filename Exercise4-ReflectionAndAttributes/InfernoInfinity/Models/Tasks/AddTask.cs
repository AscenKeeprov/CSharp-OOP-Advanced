public class AddTask : Task
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

    public AddTask(string[] parameters, IArmoury armoury, IJeweller jeweller)
	: base(parameters)
    {
	Armoury = armoury;
	Jeweller = jeweller;
    }

    public override void Execute()
    {
	string weaponName = Parameters[0];
	IWeapon weapon = Armoury.TakeWeapon(weaponName);
	if (weapon != null)
	{
	    int socketNumber = int.Parse(Parameters[1]);
	    string gemQuality = Parameters[2].Split()[0];
	    string gemType = Parameters[2].Split()[1];
	    string[] gemInfo = new string[]
	    {
	    gemQuality,
	    gemType
	    };
	    IGem gem = Jeweller.Cut(gemInfo);
	    weapon.FillSocket(socketNumber, gem);
	}
    }
}
