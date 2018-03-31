public class Knife : Weapon
{
    private const int MIN_DAMAGE = 3;
    private const int MAX_DAMAGE = 4;
    private const int SOCKETS_COUNT = 2;

    public Knife(string name, ERarity rarity) : base(name, rarity)
    {
	MinDamage = MIN_DAMAGE;
	MaxDamage = MAX_DAMAGE;
	Sockets = new IGem[SOCKETS_COUNT];
    }
}
