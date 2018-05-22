namespace Forum.App.Menus
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ViewModelContracts;
    using Forum.App.Models;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ServiceContracts;

    public class CategoriesMenu : Menu, IPaginatedMenu
    {
	private const int pageSize = 10;
	private const int categoryNameLength = 36;

	private IPostService postService;
	private ICategoryInfoViewModel[] categories;
	private int currentPage;
	private int LastPage => (categories.Length - 1) / pageSize;
	private bool IsFirstPage => currentPage == 0;
	private bool IsLastPage => currentPage == LastPage;

	public CategoriesMenu(ILabelFactory labelFactory, IPostService postService,
	    ICommandFactory commandFactory) : base(labelFactory, commandFactory)
	{
	    this.postService = postService;
	    Open();
	}

	protected override void InitializeStaticLabels(Position consoleCenter)
	{
	    string[] labelContent = new string[] { "CATEGORIES", "Name", "Posts" };
	    Position[] labelPositions = new Position[]
	    {
		new Position(consoleCenter.Left - 18, consoleCenter.Top - 12), //CATEGORIES
                new Position(consoleCenter.Left - 18, consoleCenter.Top - 10), //Name
                new Position(consoleCenter.Left + 14, consoleCenter.Top - 10), //Posts
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
		ICategoryInfoViewModel category = null;
		int categoryIndex = i + currentPage * pageSize;
		if (categoryIndex < categories.Length) category = categories[categoryIndex];
		string postsCount = category?.PostCount.ToString();
		string buffer = new string(' ', categoryNameLength - category?.Name.Length ?? 0 - postsCount?.Length ?? 0);
		string buttonText = category?.Name + buffer + postsCount;
		IButton button = labelFactory.CreateButton(buttonText, categoryButtonPositions[i], category == null);
		buttons.Add(button);
	    }
	    buttons.Add(labelFactory.CreateButton(defaultButtonContent[1], defaultButtonPositions[1], IsFirstPage));
	    buttons.Add(labelFactory.CreateButton(defaultButtonContent[2], defaultButtonPositions[2], IsLastPage));
	    Buttons = buttons.ToArray();
	}

	public override void Open()
	{
	    LoadCategories();
	    base.Open();
	}

	private void LoadCategories()
	{
	    categories = postService.GetAllCategories().ToArray();
	}

	public override IMenu ExecuteCommand()
	{
	    ICommand command = null;
	    int actualIndex = currentPage * pageSize + currentIndex;
	    if (currentIndex > 0 && currentIndex <= pageSize)
		command = commandFactory.CreateCommand(typeof(ViewCategoryMenu).Name);
	    else command = commandFactory.CreateCommand(
		String.Join(String.Empty, CurrentOption.Text.Split()));
	    return command.Execute(actualIndex.ToString());
	}

	public void ChangePage(bool forward = true)
	{
	    currentPage += forward ? 1 : -1;
	    currentIndex = 0;
	    Open();
	}
    }
}
