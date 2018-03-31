using System;
using System.Collections.Generic;
using System.Linq;

public class Armoury : IArmoury
{
    private List<IWeapon> weapons;

    public IReadOnlyList<IWeapon> Weapons
    {
	get { return weapons; }
    }

    public Armoury()
    {
	weapons = new List<IWeapon>();
    }

    public void StoreWeapon(IWeapon weapon)
    {
	if (weapons.Any(w => w.Name == weapon.Name))
	    throw new InvalidOperationException("Cannot have two weapons with the same name!");
	weapons.Add(weapon);
    }

    public IWeapon TakeWeapon(string weaponName)
    {
	IWeapon weapon = weapons.FirstOrDefault(w => w.Name == weaponName);
	//if (weapon == null)
	//    throw new ArgumentNullException("No such weapon available!");
	return weapon;
    }
}
