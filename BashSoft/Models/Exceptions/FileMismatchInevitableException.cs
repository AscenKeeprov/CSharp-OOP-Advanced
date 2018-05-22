using System;

public class FileMismatchInevitableException : Exception
{
    public override string Message => " These files are of different sizes. Mismatch is inevitable!" +
	Environment.NewLine + " Do you want to proceed with the comparison? (Y/N) ";
}
