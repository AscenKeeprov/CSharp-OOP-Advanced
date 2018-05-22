namespace Forum.App.Contracts
{
    using Forum.App.Contracts.ModelContracts;

    public interface IForumViewEngine
    {
	void RenderMenu(IMenu menu);

	void Mark(ILabel label, bool highlighted = true);

	void SetBufferHeight(int rows);

	void ResetBuffer();
    }
}
