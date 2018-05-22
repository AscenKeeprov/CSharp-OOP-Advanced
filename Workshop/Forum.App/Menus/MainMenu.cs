namespace Forum.App.Menus
{
    using System;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Models;

    public class MainMenu : Menu
    {
	private ISession session;

	public MainMenu(ISession session, ILabelFactory labelFactory, ICommandFactory commandFactory)
	    : base(labelFactory, commandFactory)
	{
	    this.session = session;
	    Open();
	}

	protected override void InitializeButtons(Position consoleCenter)
	{
	    string[] buttonContents = new string[] { "Categories", "Log In", "Sign Up" };
	    if (session?.IsLoggedIn ?? false)
	    {
		buttonContents[1] = "Add Post";
		buttonContents[2] = "Log Out";
	    }
	    Position[] buttonPositions = new Position[]
	    {
		new Position(consoleCenter.Left - 4, consoleCenter.Top - 2),
		new Position(consoleCenter.Left - 4, consoleCenter.Top + 6),
		new Position(consoleCenter.Left - 4, consoleCenter.Top + 8),
	    };
	    Buttons = new IButton[buttonContents.Length];
	    for (int b = 0; b < Buttons.Length; b++)
	    {
		Buttons[b] = labelFactory.CreateButton(buttonContents[b], buttonPositions[b]);
	    }
	}

	protected override void InitializeStaticLabels(Position consoleCenter)
	{
	    string[] labelContents = new string[]
	    {
		"FORUM",
		$"Hi, {session?.Username}"
	    };
	    Position[] labelPositions = new Position[]
	    {
		new Position(consoleCenter.Left - 4, consoleCenter.Top - 6),
		new Position(consoleCenter.Left - 4, consoleCenter.Top - 7),
	    };
	    Labels = new ILabel[labelContents.Length];
	    int lastIndex = Labels.Length - 1;
	    for (int i = 0; i < lastIndex; i++)
	    {
		Labels[i] = labelFactory.CreateLabel(labelContents[i], labelPositions[i]);
	    }
	    Labels[lastIndex] = labelFactory.CreateLabel(
		labelContents[lastIndex],
		labelPositions[lastIndex],
		!session?.IsLoggedIn ?? true);
	}

	public override IMenu ExecuteCommand()
	{
	    string commandName = String.Join(String.Empty, CurrentOption.Text.Split());
	    if (!commandName.Equals("LogOut")) commandName += typeof(Menu).Name;
	    ICommand command = commandFactory.CreateCommand(commandName);
	    IMenu menu = command.Execute();
	    return menu;
	}
    }
}
