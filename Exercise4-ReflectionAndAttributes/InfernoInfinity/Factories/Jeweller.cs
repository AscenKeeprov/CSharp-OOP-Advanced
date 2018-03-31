using System;
using System.Linq;
using System.Reflection;

public class Jeweller : IJeweller
{
    private Type[] gemTypes = Assembly.GetExecutingAssembly()
	.GetTypes().Where(t => t.BaseType == typeof(Gem)).ToArray();

    public IGem Cut(string[] gemInfo)
    {
	if (!Enum.TryParse(typeof(EQuality), gemInfo[0], true, out object quality))
	    throw new ArgumentException("Invalid gem quality!");
	Type gemType = gemTypes.SingleOrDefault(
	    t => t.Name.ToUpper() == gemInfo[1].ToUpper());
	if (gemType == null) throw new ArgumentException("Invalid gem type!");
	if (!typeof(IGem).IsAssignableFrom(gemType))
	    throw new InvalidOperationException("Gems of this type cannot be crafted!");
	IGem gem = (IGem)Activator.CreateInstance(gemType, (EQuality)quality);
	return gem;
    }
}
