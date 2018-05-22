namespace Forum.App.Contracts.FactoryContracts
{
    using Forum.App.Contracts.ModelContracts;

    public interface ICommandFactory
    {
	ICommand CreateCommand(string commandName);
    }
}
