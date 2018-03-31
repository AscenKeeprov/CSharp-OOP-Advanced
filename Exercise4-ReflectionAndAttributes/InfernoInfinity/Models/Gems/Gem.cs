public abstract class Gem : IGem
{
    public EQuality Quality { get; }
    private int bonusStrength;
    private int bonusAgility;
    private int bonusVitality;

    public int BonusStrength
    {
	get { return bonusStrength + (int)Quality; }
	protected set { bonusStrength = value; }
    }

    public int BonusAgility
    {
	get { return bonusAgility + (int)Quality; }
	protected set { bonusAgility = value; }
    }

    public int BonusVitality
    {
	get { return bonusVitality + (int)Quality; }
	protected set { bonusVitality = value; }
    }

    public Gem(EQuality quality)
    {
	Quality = quality;
    }

    public override string ToString()
    {
	return $"{Quality.ToString()} {GetType().Name} (+{BonusStrength}, +{BonusAgility}, +{BonusVitality})";
    }
}
