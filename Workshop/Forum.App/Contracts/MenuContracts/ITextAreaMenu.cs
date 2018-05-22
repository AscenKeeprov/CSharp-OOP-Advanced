namespace Forum.App.Contracts.MenuContracts
{
    using Forum.App.Contracts.ModelContracts;

    public interface ITextAreaMenu : IMenu
    {
	ITextInputArea TextArea { get; }
    }
}
