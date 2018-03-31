public class Axe : Weapon
{
    private const int MIN_DAMAGE = 5;
    private const int MAX_DAMAGE = 10;
    private const int SOCKETS_COUNT = 4;

    public Axe(string name, ERarity rarity) : base(name, rarity)
    {
	MinDamage = MIN_DAMAGE;
	MaxDamage = MAX_DAMAGE;
	Sockets = new IGem[SOCKETS_COUNT];
    }
}
