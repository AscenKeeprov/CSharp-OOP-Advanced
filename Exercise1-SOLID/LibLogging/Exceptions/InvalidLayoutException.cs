using System;

public class InvalidLayoutException : ArgumentException
{
    public override string Message => "Invalid layout!";
}
