using System.Collections.Generic;

public interface IKing : INamable, IAttackable
{
    IReadOnlyCollection<IDefender> Defenders { get; }

    void HireDefender(IDefender defender);
    IDefender CallDefender(string defenderName);
    void LoseDefender(IDefender defender);
}
