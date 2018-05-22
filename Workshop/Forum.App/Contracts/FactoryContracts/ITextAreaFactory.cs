namespace Forum.App.Contracts.FactoryContracts
{
    using Forum.App.Contracts.ModelContracts;

    public interface ITextAreaFactory
    {
	ITextInputArea CreateTextArea(IForumReader reader, int x, int y, bool isPost = true);
    }
}
