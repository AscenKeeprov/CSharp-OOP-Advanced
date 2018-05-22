using System;

public class InvalidPathException : Exception
{
    public override string Message => " The resource you are trying to access does not exist at the specified address.";
}
