using System;

public class InsufficientPrivilegesException : Exception
{
    public override string Message => " You do not have sufficient privileges to work with that resource!";
}
