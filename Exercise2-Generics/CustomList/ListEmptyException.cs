using System;

public class ListEmptyException : InvalidOperationException
{
    public override string Message => "Cannot perform operation with an empty list!";
}
