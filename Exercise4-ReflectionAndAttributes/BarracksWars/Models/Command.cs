public abstract class Command : IExecutable
{
    private string[] data;

    protected string[] Data
    {
	get { return data; }
	private set { data = value; }
    }

    public Command(string[] data)
    {
	Data = data;
    }

    public abstract string Execute();
}
