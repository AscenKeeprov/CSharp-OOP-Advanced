namespace Forum.App.Commands
{
    using Forum.App.Contracts.ModelContracts;

    public class BackCommand : Command
    {
	public BackCommand(ISession session) : base(session) { }

	public override IMenu Execute(params string[] args)
	{
	    IMenu menu = session.Back();
	    return menu;
	}
    }
}
