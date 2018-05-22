using System;

public class Footman : Defender
{
    public Footman(string name) : base(name) { }

    public override void RespondToAttack()
    {
	Console.WriteLine($"{GetType().Name} {Name} is panicking!");
    }
}
