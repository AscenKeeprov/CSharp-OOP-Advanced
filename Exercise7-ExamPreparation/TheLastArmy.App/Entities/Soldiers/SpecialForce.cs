using System.Collections.Generic;

public class SpecialForce : Soldier
{
    private const double SpecialForceSkillMiltiplier = 3.5;
    private const double SpecialForceRegenerationModifier = 30;

    private readonly List<string> weaponsAllowed = new List<string>
	{
	    "Gun",
	    "AutomaticMachine",
	    "MachineGun",
	    "RPG",
	    "Helmet",
	    "Knife",
	    "NightVision"
	};

    protected override double OverallSkillMiltiplier => SpecialForceSkillMiltiplier;
    protected override IReadOnlyList<string> WeaponsAllowed => weaponsAllowed;
    protected override double RegenerationModifier => SpecialForceRegenerationModifier;

    public SpecialForce(string name, int age, double experience, double endurance)
	: base(name, age, experience, endurance) { }
}
