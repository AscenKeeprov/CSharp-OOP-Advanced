namespace Forum.App.Commands
{
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ServiceContracts;
    using Forum.App.Menus;

    public class SignUpCommand : Command
    {
	public SignUpCommand(IUserService userService, IMenuFactory menuFactory)
	    : base(userService, menuFactory) { }

	public override IMenu Execute(params string[] args)
	{
	    string username = args[0];
	    string password = args[1];
	    userService.TrySignUpUser(username, password);
	    IMenu menu = menuFactory.CreateMenu(typeof(MainMenu).Name);
	    return menu;
	}
    }
}
