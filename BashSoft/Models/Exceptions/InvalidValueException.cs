using System;

public class InvalidValueException : ArgumentException
{
    public string ValueName { get; protected set; }
    public string ReasonForException { get; protected set; }
    public override string Message => $" {ValueName} is not valid!{ReasonForException}";

    public InvalidValueException(string valueName)
    {
	ValueName = valueName;
    }

    public InvalidValueException(string valueName, string reasonForException) : this(valueName)
    {
	if (String.IsNullOrEmpty(reasonForException))
	    ReasonForException = String.Empty;
	else ReasonForException = reasonForException;
    }
}
