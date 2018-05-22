using System;

public class DatabaseAlreadyInitializedException : Exception
{
    public override string Message => " There is information in the database already! " +
	Environment.NewLine + " Delete existing records before loading new ones? (Y/N) ";
}
