namespace Forum.App.Commands
{
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.ModelContracts;

    public class PreviousPageCommand : Command
    {
	public PreviousPageCommand(ISession session) : base(session) { }

	public override IMenu Execute(params string[] args)
	{
	    IMenu currentMenu = session.CurrentMenu;
	    if (currentMenu is IPaginatedMenu paginatedMenu)
		paginatedMenu.ChangePage(false);
	    return currentMenu;
	}
    }
}
