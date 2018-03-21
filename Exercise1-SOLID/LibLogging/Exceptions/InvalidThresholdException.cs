using System;

public class InvalidThresholdException : ArgumentException
{
    public override string Message => "Invalid report level threshold!";
}
