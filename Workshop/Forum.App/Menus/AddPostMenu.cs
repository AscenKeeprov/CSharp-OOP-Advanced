namespace Forum.App.Menus
{
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Models;
    using System;

    public class AddPostMenu : Menu, ITextAreaMenu
    {
	private ITextAreaFactory textAreaFactory;
	private IForumReader forumReader;
	private string TitleInput => Buttons[0].Text.TrimStart();
	private string CategoryInput => Buttons[1].Text.TrimStart();
	private string ErrorMessage { get; set; }
	private bool error;

	public ITextInputArea TextArea { get; private set; }

	public AddPostMenu(ILabelFactory labelFactory, ICommandFactory commandFactory,
	    ITextAreaFactory textAreaFactory, IForumReader forumReader)
	    : base(labelFactory, commandFactory)
	{
	    this.textAreaFactory = textAreaFactory;
	    this.forumReader = forumReader;
	    InitializeTextArea();
	    Open();
	}

	protected override void InitializeStaticLabels(Position consoleCenter)
	{
	    string[] labelContents = new string[] { ErrorMessage, "Title:", "Category:", " ", " " };
	    Position[] labelPositions = new Position[]
	    {
		new Position(consoleCenter.Left - 18, consoleCenter.Top - 14), //Error:
                new Position(consoleCenter.Left - 18, consoleCenter.Top - 12), //Title:
                new Position(consoleCenter.Left - 18, consoleCenter.Top - 10), //Category:
                new Position(consoleCenter.Left - 9, consoleCenter.Top - 12),  //Title:
                new Position(consoleCenter.Left - 7, consoleCenter.Top - 10),  //Category:
	    };
	    Labels = new ILabel[labelContents.Length];
	    Labels[0] = labelFactory.CreateLabel(labelContents[0], labelPositions[0], !error);
	    for (int i = 1; i < Labels.Length; i++)
	    {
		Labels[i] = labelFactory.CreateLabel(labelContents[i], labelPositions[i]);
	    }
	}

	protected override void InitializeButtons(Position consoleCenter)
	{
	    string[] buttonContents = new string[] { "Write", "Add Post" };
	    Position[] fieldPositions = new Position[]
	    {
		new Position(consoleCenter.Left - 10, consoleCenter.Top - 12), //Title: 
                new Position(consoleCenter.Left - 8, consoleCenter.Top - 10),  //Category:
	    };
	    Position[] buttonPositions = new Position[]
	    {
		new Position(consoleCenter.Left + 14, consoleCenter.Top - 8),  //Write
                new Position(consoleCenter.Left + 14, consoleCenter.Top + 12)  //Post
	    };
	    Buttons = new IButton[fieldPositions.Length + buttonPositions.Length];
	    for (int i = 0; i < fieldPositions.Length; i++)
	    {
		Buttons[i] = labelFactory.CreateButton(" ", fieldPositions[i], false, true);
	    }
	    for (int i = 0; i < buttonPositions.Length; i++)
	    {
		Buttons[i + fieldPositions.Length] = labelFactory
		    .CreateButton(buttonContents[i], buttonPositions[i]);
	    }
	    TextArea.Render();
	}

	private void InitializeTextArea()
	{
	    Position consoleCenter = Position.ConsoleCenter();
	    TextArea = textAreaFactory.CreateTextArea(forumReader, consoleCenter.Left - 18, consoleCenter.Top - 7);
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
		IMenu menu = command.Execute(TitleInput, CategoryInput, TextArea.Text);
		return menu;
	    }
	    catch (Exception exception)
	    {
		error = true;
		ErrorMessage = exception.Message;
		InitializeStaticLabels(Position.ConsoleCenter());
		return this;
	    }
	}
    }
}
