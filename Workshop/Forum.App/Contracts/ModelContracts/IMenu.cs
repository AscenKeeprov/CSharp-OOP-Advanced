namespace Forum.App.Contracts.ModelContracts
{
    public interface IMenu
    {
	ILabel[] Labels { get; }
	IButton[] Buttons { get; }
	IButton CurrentOption { get; }

	IMenu ExecuteCommand();
	void Open();
	void NextOption();
	void PreviousOption();
    }
}