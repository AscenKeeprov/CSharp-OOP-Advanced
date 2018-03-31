using System;
using System.Linq;
using System.Reflection;

public class Blacksmith : IBlacksmith
{
    private Type[] weaponTypes = Assembly.GetExecutingAssembly()
	.GetTypes().Where(t => t.BaseType == typeof(Weapon)).ToArray();

    public IWeapon Forge(string[] weaponInfo)
    {
	if (!Enum.TryParse(typeof(ERarity), weaponInfo[0], true, out object rarity))
	    throw new ArgumentException("Invalid weapon rarity!");
	Type weaponType = weaponTypes.SingleOrDefault(
	    t => t.Name.ToUpper() == weaponInfo[1].ToUpper());
	if (weaponType == null) throw new ArgumentException("Invalid weapon type!");
	if (!typeof(IWeapon).IsAssignableFrom(weaponType))
	    throw new InvalidOperationException("Weapons of this type cannot be crafted!");
	string name = weaponInfo[2];
	IWeapon weapon = (IWeapon)Activator.CreateInstance(weaponType, name, (ERarity)rarity);
	return weapon;
    }
}
