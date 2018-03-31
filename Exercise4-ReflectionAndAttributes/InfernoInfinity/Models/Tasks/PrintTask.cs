using System;

public class PrintTask : Task
{
    [Inject]
    private IArmoury armoury;

    protected IArmoury Armoury
    {
	get { return armoury; }
	set { armoury = value; }
    }

    public PrintTask(string[] parameters, IArmoury armoury)
	: base(parameters)
    {
	Armoury = armoury;
    }

    public override void Execute()
    {
	string weaponName = Parameters[0];
	IWeapon weapon = Armoury.TakeWeapon(weaponName);
	if (weapon != null) Console.WriteLine(weapon);
    }
}
