using System;

public class MissingCommandException : Exception
{
    public override string Message => " You have not entered a command.";
}
