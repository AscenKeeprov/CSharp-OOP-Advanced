namespace Forum.App.Commands
{
    using Forum.App.Contracts.FactoryContracts;

    public class LogInMenuCommand : Command
    {
	public LogInMenuCommand(IMenuFactory menuFactory) : base(menuFactory) { }
    }
}
