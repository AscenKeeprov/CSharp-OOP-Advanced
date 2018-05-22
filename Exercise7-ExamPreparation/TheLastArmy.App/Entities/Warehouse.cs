using System.Collections.Generic;
using System.Linq;

public class Warehouse : IWarehouse
{
    private IAmmunitionFactory ammunitionFactory;
    private Dictionary<string, int> ammunitions;

    public IReadOnlyDictionary<string, int> Ammunitions => ammunitions;

    public Warehouse()
    {
	ammunitionFactory = new AmmunitionFactory();
	ammunitions = new Dictionary<string, int>();
    }

    public void AddAmmunitions(string ammunitionType, int ammunitionQuantity)
    {
	if (!ammunitions.ContainsKey(ammunitionType))
	    ammunitions.Add(ammunitionType, ammunitionQuantity);
	else ammunitions[ammunitionType] += ammunitionQuantity;
    }

    public void TryEquipArmy(IArmy army)
    {
	foreach (ISoldier soldier in army.Soldiers)
	{
	    TryEquipSoldier(soldier);
	}
    }

    public bool TryEquipSoldier(ISoldier soldier)
    {
	IList<string> weaponsToEquip = soldier.Weapons
	    .Where(w => w.Value == null || w.Value.WearLevel <= 0)
	    .Select(w => w.Key).ToList();
	foreach (var weapon in weaponsToEquip)
	{
	    if (ammunitions.ContainsKey(weapon) && ammunitions[weapon] > 0)
	    {
		soldier.Weapons[weapon] = ammunitionFactory.CreateAmmunition(weapon);
		ammunitions[weapon]--;
	    }
	    else return false;
	}
	return true;
    }
}
