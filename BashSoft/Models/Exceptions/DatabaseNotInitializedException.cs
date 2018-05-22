using System;

public class DatabaseNotInitializedException : Exception
{
    public override string Message => " The database is empty! You have to load some data before running queries.";
}
