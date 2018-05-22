namespace Forum.App.Contracts.ModelContracts
{
    public interface ITextInputArea
    {
	string Text { get; }

	void Write();

	void Render();
    }
}
