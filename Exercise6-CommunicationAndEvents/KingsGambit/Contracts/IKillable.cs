public delegate void KillEventHandler(IDefender deadDefender);

public interface IKillable
{
    event KillEventHandler Killed;

    void TakeDamage();
    void Die();
}
