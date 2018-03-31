public class Sword : Weapon
{
    private const int MIN_DAMAGE = 4;
    private const int MAX_DAMAGE = 6;
    private const int SOCKETS_COUNT = 3;

    public Sword(string name, ERarity rarity) : base(name, rarity)
    {
	MinDamage = MIN_DAMAGE;
	MaxDamage = MAX_DAMAGE;
	Sockets = new IGem[SOCKETS_COUNT];
    }
}
