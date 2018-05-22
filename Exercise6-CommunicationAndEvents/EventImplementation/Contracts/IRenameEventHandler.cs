namespace EventImplementation
{
    public interface IRenameEventHandler
    {
	void OnDispatcherNameChange(object sender, NameChangeEventArgs eventArgs);
    }
}
