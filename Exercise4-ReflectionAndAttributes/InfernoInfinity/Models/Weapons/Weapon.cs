using System.Linq;
using System.Text;

public abstract class Weapon : IWeapon
{
    private const int MIN_DAMAGE_MULTIPLIER_STRENGTH = 2;
    private const int MAX_DAMAGE_MULTIPLIER_STRENGTH = 3;
    private const int MIN_DAMAGE_MULTIPLIER_AGILITY = 1;
    private const int MAX_DAMAGE_MULTIPLIER_AGILITY = 4;

    public string Name { get; }
    public ERarity Rarity { get; protected set; }
    private int minDamage;
    private int minDamageSocketModifier => GetMinDamageSocketModifier();
    private int maxDamage;
    private int maxDamageSocketModifier => GetMaxDamageSocketModifier();
    public int BonusStrength => GetBonusStrength();
    public int BonusAgility => GetBonusAgility();
    public int BonusVitality => GetBonusVitality();
    public IGem[] Sockets { get; protected set; }

    public int MinDamage
    {
	get { return minDamage * (int)Rarity + minDamageSocketModifier; }
	protected set { minDamage = value; }
    }

    public int MaxDamage
    {
	get { return maxDamage * (int)Rarity + maxDamageSocketModifier; }
	protected set { maxDamage = value; }
    }

    public Weapon(string name, ERarity rarity)
    {
	Name = name;
	Rarity = rarity;
    }

    private int GetBonusStrength()
    {
	return Sockets.Where(socket => socket != null)
	    .Sum(gems => gems.BonusStrength);
    }

    private int GetBonusAgility()
    {
	return Sockets.Where(socket => socket != null)
	    .Sum(gems => gems.BonusAgility);
    }

    private int GetBonusVitality()
    {
	return Sockets.Where(socket => socket != null)
	    .Sum(gems => gems.BonusVitality);
    }

    private int GetMinDamageSocketModifier()
    {
	int minDamageSocketModifier = 0;
	int minDamageStrengthModifier = BonusStrength * MIN_DAMAGE_MULTIPLIER_STRENGTH;
	int minDamageAgilityModifier = BonusAgility * MIN_DAMAGE_MULTIPLIER_AGILITY;
	minDamageSocketModifier += minDamageStrengthModifier + minDamageAgilityModifier;
	return minDamageSocketModifier;
    }

    private int GetMaxDamageSocketModifier()
    {
	int maxDamageSocketModifier = 0;
	int maxDamageStrengthModifier = BonusStrength * MAX_DAMAGE_MULTIPLIER_STRENGTH;
	int maxDamageAgilityModifier = BonusAgility * MAX_DAMAGE_MULTIPLIER_AGILITY;
	maxDamageSocketModifier += maxDamageStrengthModifier + maxDamageAgilityModifier;
	return maxDamageSocketModifier;
    }

    public void FillSocket(int socketNumber, IGem gem)
    {
	if (socketNumber >= 0 && socketNumber < Sockets.Length)
	    Sockets[socketNumber] = gem;
    }

    public void EmptySocket(int socketNumber)
    {
	if (socketNumber >= 0 && socketNumber < Sockets.Length)
	    Sockets[socketNumber] = null;
    }

    public override string ToString()
    {
	StringBuilder weaponInfo = new StringBuilder();
	weaponInfo.Append($"{Name}: ");
	weaponInfo.Append($"{MinDamage}-{MaxDamage} Damage");
	weaponInfo.Append($", +{BonusStrength} Strength");
	weaponInfo.Append($", +{BonusAgility} Agility");
	weaponInfo.Append($", +{BonusVitality} Vitality");
	return weaponInfo.ToString();
    }
}
