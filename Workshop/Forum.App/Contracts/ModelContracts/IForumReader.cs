namespace Forum.App.Contracts.ModelContracts
{
    public interface IForumReader
    {
	string ReadLine();

	string ReadLine(int left, int top);

	void HideCursor();

	void ShowCursor();
    }
}
