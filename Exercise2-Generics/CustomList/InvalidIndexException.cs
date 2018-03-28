using System;

public class InvalidIndexException : ArgumentException
{
    public override string Message => "Invalid index!";
}
