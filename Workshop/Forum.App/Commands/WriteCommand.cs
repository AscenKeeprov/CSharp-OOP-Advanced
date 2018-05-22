namespace Forum.App.Commands
{
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.ModelContracts;

    public class WriteCommand : Command
    {
	public WriteCommand(ISession session) : base(session) { }

	public override IMenu Execute(params string[] args)
	{
	    ITextAreaMenu currentMenu = (ITextAreaMenu)session.CurrentMenu;
	    currentMenu.TextArea.Write();
	    return currentMenu;
	}
    }
}
