namespace EventImplementation
{
    public class Dispatcher : IRenameEventRaiser
    {
	public event NameChangeEventHandler NameChange;

	private string name;

	public string Name
	{
	    get { return name; }
	    set
	    {
		OnNameChange(new NameChangeEventArgs(value));
		name = value;
	    }
	}

	public Dispatcher(string name)
	{
	    this.name = name;
	}

	public void OnNameChange(NameChangeEventArgs eventArgs)
	{
	    if (NameChange != null) NameChange.Invoke(this, eventArgs);
	}
    }
}
