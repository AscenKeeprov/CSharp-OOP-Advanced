using System;

public class InvalidAppenderException : ArgumentException
{
    public override string Message => "Invalid appender!";
}
