namespace Forum.App.Menus
{
    using System.Linq;
    using System.Collections.Generic;
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts;
    using Forum.App.Contracts.ViewModelContracts;
    using Forum.App.Models;
    using Forum.App.Contracts.ServiceContracts;
    using System;

    public class ViewPostMenu : Menu, IIdHoldingMenu
    {
	private const int LEFT_OFFSET = 18;
	private const int TOP_OFFSET = 7;

	private ISession session;
	private IPostService postService;
	private IForumViewEngine viewEngine;
	private IPostViewModel postViewModel;
	private int postId;

	public ViewPostMenu(ILabelFactory labelFactory, ICommandFactory commandFactory,
	    ISession session, IPostService postService, IForumViewEngine viewEngine)
	    : base(labelFactory, commandFactory)
	{
	    this.session = session;
	    this.postService = postService;
	    this.viewEngine = viewEngine;
	}

	protected override void InitializeStaticLabels(Position consoleCenter)
	{
	    Position titlePosition = new Position(consoleCenter.Left - postViewModel.Title.Length / 2, consoleCenter.Top - 10);
	    Position authorPosition = new Position(consoleCenter.Left - postViewModel.Author.Length, consoleCenter.Top - 9);
	    List<ILabel> labels = new List<ILabel>()
	    {
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
	    Buttons = new IButton[2];
	    Buttons[0] = labelFactory.CreateButton("Back",
		new Position(consoleCenter.Left + 15, consoleCenter.Top - 3));
	    Buttons[1] = labelFactory.CreateButton("Add Reply",
		    new Position(consoleCenter.Left + 10, consoleCenter.Top - 4), !session.IsLoggedIn);
	}

	public void SetId(int id)
	{
	    postId = id;
	    Open();
	}

	public override void Open()
	{
	    LoadPost();
	    ExtendBuffer();
	    base.Open();
	    Position consoleCenter = Position.ConsoleCenter();
	    int currentRow = consoleCenter.Top - (consoleCenter.Top - TOP_OFFSET - 3 - postViewModel.Content.Length) + 1;
	    Position repliesStartPosition = new Position(consoleCenter.Left - LEFT_OFFSET, currentRow++);
	    int repliesCount = postViewModel.Replies.Length;
	    ICollection<ILabel> replyLabels = new List<ILabel>();
	    replyLabels.Add(labelFactory.CreateLabel($"Replies: {repliesCount}", repliesStartPosition));
	    foreach (var reply in postViewModel.Replies)
	    {
		Position replyAuthorPosition = new Position(repliesStartPosition.Left, ++currentRow);
		replyLabels.Add(labelFactory.CreateLabel(reply.Author, replyAuthorPosition));
		foreach (var line in reply.Content)
		{
		    Position rowPosition = new Position(repliesStartPosition.Left, ++currentRow);
		    replyLabels.Add(labelFactory.CreateLabel(line, rowPosition));
		}
		currentRow++;
	    }
	    Labels = Labels.Concat(replyLabels).ToArray();
	}

	private void LoadPost()
	{
	    postViewModel = postService.GetPostViewModel(postId);
	}

	private void ExtendBuffer()
	{
	    int totalLines = 13 + postViewModel.Content.Length;
	    foreach (var reply in postViewModel.Replies)
	    {
		totalLines += 2 + reply.Content.Length;
	    }
	    if (totalLines > 30)
	    {
		viewEngine.SetBufferHeight(totalLines);
	    }
	}

	public override IMenu ExecuteCommand()
	{
	    string commandName = String.Join(String.Empty, CurrentOption.Text.Split());
	    if (commandName.Equals("AddReply")) commandName += typeof(Menu).Name;
	    ICommand command = commandFactory.CreateCommand(commandName);
	    IMenu menu = command.Execute(postId.ToString());
	    viewEngine.ResetBuffer();
	    return menu;
	}
    }
}
