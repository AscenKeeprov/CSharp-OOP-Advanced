using System;

public class InvalidCommandParameterException : ArgumentException
{
    private string parameter;
    public override string Message => $" {parameter} is not in the expected format!";

    public InvalidCommandParameterException(string parameter)
    {
	if (String.IsNullOrEmpty(parameter))
	    this.parameter = "One of the command parameters";
	else this.parameter = parameter;
    }
}
