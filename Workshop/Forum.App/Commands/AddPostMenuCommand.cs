namespace Forum.App.Commands
{
    using Forum.App.Contracts.FactoryContracts;

    public class AddPostMenuCommand : Command
    {
	public AddPostMenuCommand(IMenuFactory menuFactory) : base(menuFactory) { }
    }
}
