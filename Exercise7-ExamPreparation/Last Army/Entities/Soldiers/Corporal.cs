using System.Collections.Generic;

public class Corporal : Soldier
{
    private const double CorporalSkillMiltiplier = 2.5;

    private readonly List<string> weaponsAllowed = new List<string>
	{
	    "Gun",
	    "AutomaticMachine",
	    "MachineGun",
	    "Helmet",
	    "Knife"
	};

    protected override double OverallSkillMiltiplier => CorporalSkillMiltiplier;
    protected override IReadOnlyList<string> WeaponsAllowed => weaponsAllowed;

    public Corporal(string name, int age, double experience, double endurance)
	: base(name, age, experience, endurance) { }
}
