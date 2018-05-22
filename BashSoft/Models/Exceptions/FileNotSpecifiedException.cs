using System;

public class FileNotSpecifiedException : Exception
{
    public override string Message => " The provided path does not point to a file!";
}
