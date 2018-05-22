using System;

public class MissingCommandParameterException : Exception
{
    public override string Message => " A required command parameter is missing!";
}
