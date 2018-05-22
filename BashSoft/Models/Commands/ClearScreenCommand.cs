using System;

[Alias("WIPE")]
public class ClearScreenCommand : Command
{
    internal override int MaxAllowedParameters => 1;

    public ClearScreenCommand(string[] parameters) : base(parameters) { }

    public override void Execute()
    {
	Validate(Parameters);
	Console.Clear();
    }
}
