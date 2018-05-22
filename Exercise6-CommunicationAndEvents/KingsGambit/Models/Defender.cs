public abstract class Defender : IDefender
{
    private const int DEFAULT_DEFENDER_HEALTH = 2;

    public event KillEventHandler Killed;

    public string Name { get; }
    public virtual int Health { get; private set; }
    public virtual int Armour { get; protected set; }

    protected Defender(string name)
    {
	Name = name;
	Health = DEFAULT_DEFENDER_HEALTH;
    }

    public abstract void RespondToAttack();

    public void TakeDamage()
    {
	if (Armour > 0) Armour--;
	else Health--;
	if (Health == 0) Die();
    }

    public void Die()
    {
	if (Killed != null) Killed.Invoke(this);
    }
}
