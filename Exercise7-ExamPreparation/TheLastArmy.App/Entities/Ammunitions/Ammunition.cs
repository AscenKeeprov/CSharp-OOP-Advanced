public abstract class Ammunition : IAmmunition
{
    private const double WearLevelMultiplier = 100;

    public string Name => GetType().Name;
    public abstract double Weight { get; }
    public double WearLevel { get; private set; }

    protected Ammunition()
    {
	WearLevel = Weight * WearLevelMultiplier;
    }

    public void DecreaseWearLevel(double wearAmount)
    {
	WearLevel -= wearAmount;
    }
}
