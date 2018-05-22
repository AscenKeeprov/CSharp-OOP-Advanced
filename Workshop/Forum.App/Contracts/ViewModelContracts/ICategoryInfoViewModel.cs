namespace Forum.App.Contracts.ViewModelContracts
{
    public interface ICategoryInfoViewModel
    {
	int Id { get; }
	string Name { get; }
	int PostCount { get; }
    }
}
