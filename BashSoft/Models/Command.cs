using System.Text;

public abstract class Command : ICommand
{
    private string[] parameters;
    internal virtual int MinRequiredParameters => 1;
    internal virtual int MaxAllowedParameters => 5;

    public string[] Parameters
    {
	get { return parameters; }
	private set
	{
	    if (value == null || value.Length == 0)
		throw new MissingCommandParameterException();
	    parameters = value;
	}
    }

    public Command(string[] parameters)
    {
	Parameters = parameters;
    }

    public void Validate(string[] parameters)
    {
	if (parameters.Length < MinRequiredParameters)
	    throw new MissingCommandParameterException();
	else if (parameters.Length > MaxAllowedParameters)
	    throw new RedundantCommandParameterException(parameters[0].ToUpper());
    }

    public abstract void Execute();

    public override string ToString()
    {
	StringBuilder commandInfo = new StringBuilder();
	for (int i = 0; i < Parameters.Length; i++)
	{
	    if (Parameters[i].Contains(" "))
		commandInfo.Append($"\"{Parameters[i]}\" ");
	    else commandInfo.Append($"{Parameters[i]} ");
	}
	return commandInfo.ToString().TrimEnd();
    }
}
