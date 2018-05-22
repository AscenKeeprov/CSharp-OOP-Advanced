using System;
using System.Linq;
using System.Reflection;

public class AmmunitionFactory : IAmmunitionFactory
{
    public IAmmunition CreateAmmunition(string ammunitionType)
    {
	Type typeOfAmmunition = Assembly.GetCallingAssembly().GetTypes()
	    .SingleOrDefault(type => type.Name == ammunitionType);
	if (typeOfAmmunition == null) throw new ArgumentException(
	    String.Format(OutputMessages.MissingAmmunitionType, ammunitionType));
	if (!typeof(IAmmunition).IsAssignableFrom(typeOfAmmunition))
	    throw new InvalidOperationException(
		String.Format(OutputMessages.InvalidAmmunitionType, ammunitionType));
	IAmmunition ammunition = (IAmmunition)Activator.CreateInstance(typeOfAmmunition);
	return ammunition;
    }
}
