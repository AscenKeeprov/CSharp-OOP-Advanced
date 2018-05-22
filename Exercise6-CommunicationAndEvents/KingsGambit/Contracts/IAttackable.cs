public delegate void AttackEventHandler();

public interface IAttackable
{
    event AttackEventHandler Attacked;

    void ReportAttack();
}
