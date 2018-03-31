public class Emerald : Gem
{
    private const int BONUS_STRENGTH = 1;
    private const int BONUS_AGILITY = 4;
    private const int BONUS_VITALITY = 9;

    public Emerald(EQuality quality) : base(quality)
    {
	BonusStrength = BONUS_STRENGTH;
	BonusAgility = BONUS_AGILITY;
	BonusVitality = BONUS_VITALITY;
    }
}
