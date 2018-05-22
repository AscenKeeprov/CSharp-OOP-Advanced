using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Soldier : ISoldier
{
    private const double MaxEndurance = 100;
    private const double SoldierRegenerationModifier = 10;

    private double endurance;

    public string Name { get; }
    public int Age { get; }
    public double Experience { get; private set; }
    public double Endurance
    {
	get { return endurance; }
	protected set
	{
	    endurance = Math.Min(value, MaxEndurance);
	}
    }
    protected abstract double OverallSkillMiltiplier { get; }
    public double OverallSkill => (Age + Experience) * OverallSkillMiltiplier;
    public IDictionary<string, IAmmunition> Weapons { get; }
    protected abstract IReadOnlyList<string> WeaponsAllowed { get; }
    protected virtual double RegenerationModifier => SoldierRegenerationModifier;

    protected Soldier(string name, int age, double experience, double endurance)
    {
	Name = name;
	Age = age;
	Experience = experience;
	Endurance = endurance;
	Weapons = new Dictionary<string, IAmmunition>();
	foreach (var weapon in WeaponsAllowed)
	{
	    Weapons.Add(weapon, null);
	}
    }

    public bool ReadyForMission(IMission mission)
    {
	if (Endurance < mission.EnduranceRequired) return false;
	if (Weapons.Values.Any(weapon => weapon == null)) return false;
	return Weapons.Values.All(weapon => weapon.WearLevel > 0);
    }

    private void AmmunitionRevision(double missionWearLevelDecrement)
    {
	IEnumerable<string> weapons = Weapons.Keys
	    .Where(k => Weapons[k] != null).ToList();
	foreach (string weapon in weapons)
	{
	    Weapons[weapon].DecreaseWearLevel(missionWearLevelDecrement);
	    if (Weapons[weapon].WearLevel <= 0) Weapons[weapon] = null;
	}
    }

    public void Regenerate()
    {
	Endurance += Age + RegenerationModifier;
    }

    public void CompleteMission(IMission mission)
    {
	Experience += mission.EnduranceRequired;
	Endurance -= mission.EnduranceRequired;
	AmmunitionRevision(mission.WearLevelDecrement);
    }

    public override string ToString() => string.Format(
	OutputMessages.SoldierToString, Name, OverallSkill);
}
