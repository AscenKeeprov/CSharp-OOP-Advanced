public class Amethyst : Gem
{
    private const int BONUS_STRENGTH = 2;
    private const int BONUS_AGILITY = 8;
    private const int BONUS_VITALITY = 4;

    public Amethyst(EQuality quality) : base(quality)
    {
	BonusStrength = BONUS_STRENGTH;
	BonusAgility = BONUS_AGILITY;
	BonusVitality = BONUS_VITALITY;
    }
}
