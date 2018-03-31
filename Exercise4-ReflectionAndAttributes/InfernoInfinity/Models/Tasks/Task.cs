public abstract class Task : IExecutable
{
    private string[] parameters;

    protected string[] Parameters
    {
	get { return parameters; }
	set { parameters = value; }
    }

    public Task(string[] parameters)
    {
	Parameters = parameters;
    }

    public abstract void Execute();
}
