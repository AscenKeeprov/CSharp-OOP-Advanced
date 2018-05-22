using System;
using System.Collections.Generic;
using System.Linq;

public class King : IKing
{
    public event AttackEventHandler Attacked;

    public string Name { get; }
    private ICollection<IDefender> defenders;

    public IReadOnlyCollection<IDefender> Defenders => (IReadOnlyCollection<IDefender>)defenders;

    public King(string name)
    {
	Name = name;
	defenders = new List<IDefender>();
    }

    public void HireDefender(IDefender defender)
    {
	defenders.Add(defender);
	Attacked += defender.RespondToAttack;
	defender.Killed += LoseDefender;
    }

    public IDefender CallDefender(string defenderName)
    {
	IDefender defender = defenders
	    .SingleOrDefault(d => d.Name == defenderName);
	return defender;
    }

    public void ReportAttack()
    {
	Console.WriteLine($"{GetType().Name} {Name} is under attack!");
	if (Attacked != null) Attacked.Invoke();
    }

    public void LoseDefender(IDefender deadDefender)
    {
	Attacked -= deadDefender.RespondToAttack;
	defenders.Remove(deadDefender);
    }
}
