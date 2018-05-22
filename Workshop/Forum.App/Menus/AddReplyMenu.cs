namespace Forum.App.Menus
{
    using System.Collections.Generic;
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ViewModelContracts;
    using Forum.App.Models;
    using System;
    using Forum.App.Contracts.ServiceContracts;

    public class AddReplyMenu : Menu, ITextAreaMenu, IIdHoldingMenu
    {
	private const int LEFT_OFFSET = 18;
	private const int TOP_OFFSET = 7;
	private const int BUTTON_OFFSET = 14;

	private ITextAreaFactory textAreaFactory;
	private IForumReader forumReader;
	private IPostService postService;
	private IPostViewModel postViewModel;
	private string ErrorMessage { get; set; }
	private int postId;
	private bool error;

	public ITextInputArea TextArea { get; private set; }

	public AddReplyMenu(ILabelFactory labelFactory, ICommandFactory commandFactory,
	    ITextAreaFactory textAreaFactory, IForumReader forumReader, IPostService postService)
	    : base(labelFactory, commandFactory)
	{
	    this.textAreaFactory = textAreaFactory;
	    this.forumReader = forumReader;
	    this.postService = postService;
	}

	protected override void InitializeStaticLabels(Position consoleCenter)
	{
	    Position errorPosition =
		new Position(consoleCenter.Left - postViewModel.Title.Length / 2, consoleCenter.Top - 12);
	    Position titlePosition =
		new Position(consoleCenter.Left - postViewModel.Title.Length / 2, consoleCenter.Top - 10);
	    Position authorPosition =
		new Position(consoleCenter.Left - postViewModel.Author.Length, consoleCenter.Top - 9);
	    List<ILabel> labels = new List<ILabel>()
	    {
		labelFactory.CreateLabel(ErrorMessage, errorPosition, !error),
		labelFactory.CreateLabel(postViewModel.Title, titlePosition),
		labelFactory.CreateLabel($"Author: {postViewModel.Author}", authorPosition)
	    };
	    int leftPosition = consoleCenter.Left - LEFT_OFFSET;
	    int lineCount = postViewModel.Content.Length;
	    for (int i = 0; i < lineCount; i++)
	    {
		Position position = new Position(leftPosition, consoleCenter.Top - (TOP_OFFSET - i));
		ILabel label = labelFactory.CreateLabel(postViewModel.Content[i], position);
		labels.Add(label);
	    }
	    Labels = labels.ToArray();
	}

	protected override void InitializeButtons(Position consoleCenter)
	{
	    int left = consoleCenter.Left + BUTTON_OFFSET;
	    int top = consoleCenter.Top - (TOP_OFFSET - postViewModel.Content.Length);
	    Buttons = new IButton[3];
	    Buttons[0] = labelFactory.CreateButton("Write", new Position(left, top + 1));
	    Buttons[1] = labelFactory.CreateButton("Add Reply", new Position(left - 1, top + 11));
	    Buttons[2] = labelFactory.CreateButton("Back", new Position(left + 1, top + 12));
	    TextArea.Render();
	}

	public override void Open()
	{
	    LoadPost();
	    InitializeTextArea();
	    base.Open();
	}

	private void LoadPost()
	{
	    postViewModel = postService.GetPostViewModel(postId);
	}

	private void InitializeTextArea()
	{
	    Position consoleCenter = Position.ConsoleCenter();
	    int top = consoleCenter.Top - (TOP_OFFSET + postViewModel.Content.Length) + 5;
	    TextArea = textAreaFactory.CreateTextArea(forumReader, consoleCenter.Left - 18, top, false);
	}

	public void SetId(int id)
	{
	    postId = id;
	    Open();
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
		IMenu menu = command.Execute(postId.ToString(), TextArea.Text);
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
