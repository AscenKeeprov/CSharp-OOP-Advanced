namespace Forum.App.Commands
{
    using Forum.App.Contracts.FactoryContracts;

    public class CategoriesMenuCommand : Command
    {
	public CategoriesMenuCommand(IMenuFactory menuFactory) : base(menuFactory) { }
    }
}
