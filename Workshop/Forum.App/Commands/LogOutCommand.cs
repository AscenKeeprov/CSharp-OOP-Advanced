namespace Forum.App.Commands
{
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ServiceContracts;

    public class LogOutCommand : Command
    {
	public LogOutCommand(ISession session, IUserService userService)
	    : base(session, userService) { }

	public override IMenu Execute(params string[] args)
	{
	    IMenu menu = session.CurrentMenu;
	    userService.LogOutUser();
	    menu.Open();
	    return menu;
	}
    }
}
