using System;

public class NonExecutableCommandException : Exception
{
    private string command;
    public override string Message => $"{command} is not an executable command.";

    public NonExecutableCommandException(string command)
    {
	this.command = command;
    }
}
