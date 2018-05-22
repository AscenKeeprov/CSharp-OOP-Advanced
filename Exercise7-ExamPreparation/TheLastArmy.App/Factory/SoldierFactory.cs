using System;
using System.Linq;
using System.Reflection;

public class SoldierFactory : ISoldierFactory
{
    public ISoldier CreateSoldier(string soldierType, string name, int age, double experience, double endurance)
    {
	Type rank = Assembly.GetCallingAssembly().GetTypes()
	    .SingleOrDefault(type => type.Name == soldierType);
	if (rank == null) throw new ArgumentException(
	    String.Format(OutputMessages.MissingSoldierType, soldierType));
	if (!typeof(ISoldier).IsAssignableFrom(rank))
	    throw new InvalidOperationException(
		String.Format(OutputMessages.InvalidSoldierType, soldierType));
	ISoldier soldier = (ISoldier)Activator.CreateInstance(rank, name, age, experience, endurance);
	return soldier;
    }
}
