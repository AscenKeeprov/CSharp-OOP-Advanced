namespace Forum.App.Contracts.ModelContracts
{
    public interface ICommand
    {
	IMenu Execute(params string[] args);
    }
}
