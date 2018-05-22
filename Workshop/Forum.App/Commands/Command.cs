namespace Forum.App.Commands
{
    using System;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ServiceContracts;

    public abstract class Command : ICommand
    {
	protected ISession session;
	protected IUserService userService;
	protected IPostService postService;
	protected IMenuFactory menuFactory;
	protected ICommandFactory commandFactory;

	protected Command(ISession session)
	{
	    this.session = session;
	}

	protected Command(IMenuFactory menuFactory)
	{
	    this.menuFactory = menuFactory;
	}

	protected Command(ISession session, IUserService userService) : this(session)
	{
	    this.userService = userService;
	}

	protected Command(IUserService userService, IMenuFactory menuFactory)
	    : this(menuFactory)
	{
	    this.userService = userService;
	}

	protected Command(ISession session, IPostService postService, ICommandFactory commandFactory)
	    : this(session)
	{
	    this.postService = postService;
	    this.commandFactory = commandFactory;
	}

	public virtual IMenu Execute(params string[] args)
	{
	    string commandName = GetType().Name;
	    string menuName = commandName.Replace(GetType().BaseType.Name, String.Empty);
	    IMenu menu = menuFactory.CreateMenu(menuName);
	    return menu;
	}
    }
}
