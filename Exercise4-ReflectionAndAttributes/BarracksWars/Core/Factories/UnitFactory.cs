using System;

public class UnitFactory : IUnitFactory
{
    public IUnit CreateUnit(string unitType)
    {
	Type typeOfUnit = Type.GetType(unitType);
	IUnit unit = (IUnit)Activator.CreateInstance(typeOfUnit);
	return unit;
    }
}
