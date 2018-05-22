namespace Forum.App.Menus
{
    using System;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Models;

    public class SignUpMenu : Menu
    {
	private IForumReader forumReader;
	private string UsernameInput => Buttons[0].Text.TrimStart();
	private string PasswordInput => Buttons[1].Text.TrimStart();
	private string ErrorMessage { get; set; }
	private bool error;

	public SignUpMenu(ILabelFactory labelFactory, ICommandFactory commandFactory,
	    IForumReader forumReader) : base(labelFactory, commandFactory)
	{
	    this.forumReader = forumReader;
	    Open();
	}

	protected override void InitializeStaticLabels(Position consoleCenter)
	{
	    string[] labelContents = new string[] { ErrorMessage, "Name:", "Password:" };
	    Position[] labelPositions = new Position[]
	    {
		new Position(consoleCenter.Left - ErrorMessage?.Length / 2 ?? 0, consoleCenter.Top - 13),   //Error: 
                new Position(consoleCenter.Left - 16, consoleCenter.Top - 10),   //Name:
                new Position(consoleCenter.Left - 16, consoleCenter.Top - 8),    //Password:
	    };
	    Labels = new ILabel[labelContents.Length];
	    Labels[0] = new Label(labelContents[0], labelPositions[0], !error);
	    for (int i = 1; i < Labels.Length; i++)
	    {
		Labels[i] = new Label(labelContents[i], labelPositions[i]);
	    }
	}

	protected override void InitializeButtons(Position consoleCenter)
	{
	    string[] buttonContents = new string[] { " ", " ", "Sign Up", "Back" };
	    Position[] buttonPositions = new Position[]
	    {
		new Position(consoleCenter.Left - 10, consoleCenter.Top - 10), //Name
                new Position(consoleCenter.Left - 6, consoleCenter.Top - 8),   //Password
                new Position(consoleCenter.Left + 16, consoleCenter.Top),      //Sign Up
                new Position(consoleCenter.Left + 16, consoleCenter.Top + 1)   //Back
	    };
	    Buttons = new IButton[buttonContents.Length];
	    for (int i = 0; i < Buttons.Length; i++)
	    {
		string buttonContent = buttonContents[i];
		bool isField = string.IsNullOrWhiteSpace(buttonContent);
		Buttons[i] = labelFactory.CreateButton(buttonContent, buttonPositions[i], false, isField);
	    }
	}

	public override IMenu ExecuteCommand()
	{
	    if (CurrentOption.IsField)
	    {
		string fieldInput = forumReader.ReadLine(
		    CurrentOption.Position.Left + 1, CurrentOption.Position.Top);
		Buttons[currentIndex] = labelFactory.CreateButton(
		    fieldInput, CurrentOption.Position, CurrentOption.IsHidden, CurrentOption.IsField);
		return this;
	    }
	    try
	    {
		string commandName = String.Join(String.Empty, CurrentOption.Text.Split());
		ICommand command = commandFactory.CreateCommand(commandName);
		IMenu menu = command.Execute(UsernameInput, PasswordInput);
		return menu;
	    }
	    catch (Exception exception)
	    {
		error = true;
		ErrorMessage = exception.Message;
		Open();
		return this;
	    }
	}
    }
}
