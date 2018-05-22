namespace Forum.App.Commands
{
    using Forum.App.Contracts.FactoryContracts;

    public class SignUpMenuCommand : Command
    {
	public SignUpMenuCommand(IMenuFactory menuFactory) : base(menuFactory) { }
    }
}
