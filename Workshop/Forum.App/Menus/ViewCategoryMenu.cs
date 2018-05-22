namespace Forum.App.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ServiceContracts;
    using Forum.App.Contracts.ViewModelContracts;
    using Forum.App.Models;

    public class ViewCategoryMenu : Menu, IIdHoldingMenu, IPaginatedMenu
    {
	private const int pageSize = 10;
	private const int categoryNameLength = 36;

	private IPostService postService;
	private IPostInfoViewModel[] posts;
	private int categoryId;
	private int currentPage;
	private int LastPage => (posts.Length - 1) / pageSize;
	private bool IsFirstPage => currentPage == 0;
	private bool IsLastPage => currentPage == LastPage;

	public ViewCategoryMenu(ILabelFactory labelFactory, ICommandFactory commandFactory,
	    IPostService postService) : base(labelFactory, commandFactory)
	{
	    this.postService = postService;
	}

	public void SetId(int id)
	{
	    categoryId = id;
	    Open();
	}

	public override void Open()
	{
	    LoadPosts();
	    base.Open();
	}

	private void LoadPosts()
	{
	    posts = postService.GetCategoryPostsInfo(categoryId).ToArray();
	}

	protected override void InitializeStaticLabels(Position consoleCenter)
	{
	    string categoryName = postService.GetCategoryName(categoryId);
	    string[] labelContent = new string[] { categoryName, "Name", "Replies" };
	    Position[] labelPositions = new Position[]
	    {
		new Position(consoleCenter.Left - 18, consoleCenter.Top - 12), //Category name
                new Position(consoleCenter.Left - 18, consoleCenter.Top - 10), //Name
                new Position(consoleCenter.Left + 12, consoleCenter.Top - 10), //Replies
	    };
	    Labels = new ILabel[labelContent.Length];
	    for (int i = 0; i < Labels.Length; i++)
	    {
		Labels[i] = labelFactory.CreateLabel(labelContent[i], labelPositions[i]);
	    }
	}

	protected override void InitializeButtons(Position consoleCenter)
	{
	    string[] defaultButtonContent = new string[] { "Back", "Previous Page", "Next Page" };
	    Position[] defaultButtonPositions = new Position[]
	    {
		new Position(consoleCenter.Left + 15, consoleCenter.Top - 12), //Back   
                new Position(consoleCenter.Left - 18, consoleCenter.Top + 12), //Previous Page
                new Position(consoleCenter.Left + 10, consoleCenter.Top + 12), //Next Page
	    };
	    Position[] categoryButtonPositions = new Position[pageSize];
	    for (int i = 0; i < pageSize; i++)
	    {
		categoryButtonPositions[i] = new Position(consoleCenter.Left - 18, consoleCenter.Top - 8 + i * 2);
	    }
	    IList<IButton> buttons = new List<IButton>();
	    buttons.Add(labelFactory.CreateButton(defaultButtonContent[0], defaultButtonPositions[0]));
	    for (int i = 0; i < categoryButtonPositions.Length; i++)
	    {
		IPostInfoViewModel post = null;
		int categoryIndex = i + currentPage * pageSize;
		if (categoryIndex < posts.Length) post = posts[categoryIndex];
		string postsCount = post?.ReplyCount.ToString();
		string buffer = new string(' ', categoryNameLength - post?.Title.Length ?? 0 - postsCount?.Length ?? 0);
		string buttonText = post?.Title + buffer + postsCount;
		IButton button = labelFactory.CreateButton(buttonText, categoryButtonPositions[i], post == null);
		buttons.Add(button);
	    }
	    buttons.Add(labelFactory.CreateButton(defaultButtonContent[1], defaultButtonPositions[1], IsFirstPage));
	    buttons.Add(labelFactory.CreateButton(defaultButtonContent[2], defaultButtonPositions[2], IsLastPage));
	    Buttons = buttons.ToArray();
	}

	public override IMenu ExecuteCommand()
	{
	    string commandName = String.Join(String.Empty, CurrentOption.Text.Split());
	    ICommand command = null;
	    int? postId = null;
	    if (currentIndex > 0 && currentIndex <= pageSize)
	    {
		int actualIndex = currentPage * pageSize + currentIndex - 1;
		postId = posts[actualIndex].Id;
		command = commandFactory.CreateCommand(typeof(ViewPostMenu).Name);
	    }
	    else command = commandFactory.CreateCommand(commandName);
	    IMenu menu = command.Execute(postId.ToString());
	    return menu;
	}

	public void ChangePage(bool forward = true)
	{
	    currentPage += forward ? 1 : -1;
	    currentIndex = 0;
	    Open();
	}
    }
}
