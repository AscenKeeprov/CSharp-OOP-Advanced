using System;

public class DatabaseSourceEmptyException : Exception
{
    public override string Message => " The provided database source file contains no information!";
}
