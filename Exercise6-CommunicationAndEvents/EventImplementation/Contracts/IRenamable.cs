namespace EventImplementation
{
    public delegate void NameChangeEventHandler(object sender, NameChangeEventArgs eventArgs);

    public interface IRenamable
    {
	event NameChangeEventHandler NameChange;

	void OnNameChange(NameChangeEventArgs eventArgs);
    }
}
