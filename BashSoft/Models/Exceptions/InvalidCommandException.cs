using System;

public class InvalidCommandException : ArgumentException
{
    private string command;
    public override string Message => $" {command} is not a valid command";

    public InvalidCommandException(string command)
    {
	this.command = command;
    }
}
