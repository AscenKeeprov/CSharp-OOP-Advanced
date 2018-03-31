public class Ruby : Gem
{
    private const int BONUS_STRENGTH = 7;
    private const int BONUS_AGILITY = 2;
    private const int BONUS_VITALITY = 5;

    public Ruby(EQuality quality) : base(quality)
    {
	BonusStrength = BONUS_STRENGTH;
	BonusAgility = BONUS_AGILITY;
	BonusVitality = BONUS_VITALITY;
    }
}
