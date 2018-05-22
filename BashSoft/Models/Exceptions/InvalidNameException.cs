public class InvalidNameException : InvalidValueException
{
    public InvalidNameException(string resourceType) : base(resourceType)
    {
	ValueName = $"{resourceType} name";
	ReasonForException = " It cannot contain any of the following symbols: \\/:*?\"<>|";
    }
}
