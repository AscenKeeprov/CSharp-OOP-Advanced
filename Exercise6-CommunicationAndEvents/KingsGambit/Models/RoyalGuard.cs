using System;

public class RoyalGuard : Defender
{
    private const int DEFAULT_DEFENDER_ARMOUR = 1;

    public RoyalGuard(string name) : base(name)
    {
	Armour = DEFAULT_DEFENDER_ARMOUR;
    }

    public override void RespondToAttack()
    {
	Console.WriteLine($"Royal Guard {Name} is defending!");
    }
}
