namespace Forum.App.Menus
{
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Models;

    public abstract class Menu : IMenu
    {
	protected int currentIndex;
	protected ILabelFactory labelFactory;
	protected ICommandFactory commandFactory;
	public ILabel[] Labels { get; protected set; }
	public IButton[] Buttons { get; protected set; }
	public IButton CurrentOption => Buttons[currentIndex];

	protected Menu(ILabelFactory labelFactory, ICommandFactory commandFactory)
	{
	    this.labelFactory = labelFactory;
	    this.commandFactory = commandFactory;
	}

	public virtual void Open()
	{
	    Position consoleCenter = Position.ConsoleCenter();
	    InitializeStaticLabels(consoleCenter);
	    InitializeButtons(consoleCenter);
	}

	protected abstract void InitializeStaticLabels(Position consoleCenter);

	protected abstract void InitializeButtons(Position consoleCenter);

	public void NextOption()
	{
	    currentIndex++;
	    int totalOptions = Buttons.Length;
	    if (currentIndex >= totalOptions)
		currentIndex -= totalOptions;
	    if (CurrentOption.IsHidden) NextOption();
	}

	public void PreviousOption()
	{
	    currentIndex--;
	    if (currentIndex < 0)
		currentIndex += Buttons.Length;
	    if (CurrentOption.IsHidden) PreviousOption();
	}

	public abstract IMenu ExecuteCommand();
    }
}
