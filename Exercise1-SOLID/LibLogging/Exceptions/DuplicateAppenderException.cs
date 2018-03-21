using System;

public class DuplicateAppenderException : Exception
{
    public override string Message => "Duplicate appender!";
}
