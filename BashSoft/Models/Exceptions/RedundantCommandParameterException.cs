using System;

public class RedundantCommandParameterException : Exception
{
    private string commandName;
    public override string Message => $" Command {commandName} does not work with that many parameters!";

    public RedundantCommandParameterException(string commandName)
    {
	this.commandName = commandName;
    }
}
