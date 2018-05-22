namespace Forum.App
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts;
    using Forum.App.Menus;

    internal class MenuController : IMainController
    {
	private IServiceProvider serviceProvider;
	private IForumViewEngine viewEngine;
	private ISession session;
	private ICommandFactory commandFactory;
	private IMenu CurrentMenu => session.CurrentMenu;

	public MenuController(IServiceProvider serviceProvider, IForumViewEngine viewEngine, ISession session, ICommandFactory commandFactory)
	{
	    this.serviceProvider = serviceProvider;
	    this.viewEngine = viewEngine;
	    this.session = session;
	    this.commandFactory = commandFactory;
	    InitializeSession();
	}

	private void InitializeSession()
	{
	    IMenu mainMenu = new MainMenu(session,
		serviceProvider.GetService<ILabelFactory>(),
		serviceProvider.GetService<ICommandFactory>());
	    session.PushView(mainMenu);
	    RenderCurrentView();
	}

	private void RenderCurrentView()
	{
	    this.viewEngine.RenderMenu(this.CurrentMenu);
	}

	public void MarkOption()
	{
	    this.viewEngine.Mark(this.CurrentMenu.CurrentOption);
	}

	public void UnmarkOption()
	{
	    this.viewEngine.Mark(this.CurrentMenu.CurrentOption, false);
	}

	public void NextOption()
	{
	    this.CurrentMenu.NextOption();
	}

	public void PreviousOption()
	{
	    this.CurrentMenu.PreviousOption();
	}

	public void Back()
	{
	    this.session.Back();
	    RenderCurrentView();
	}

	public void Execute()
	{
	    this.session.PushView(this.CurrentMenu.ExecuteCommand());
	    this.RenderCurrentView();
	}
    }
}