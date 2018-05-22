namespace Forum.App.ViewModels
{
    using Forum.App.Contracts.ViewModelContracts;

    public class CategoryInfoViewModel : ICategoryInfoViewModel
    {
	public int Id { get; }
	public string Name { get; }
	public int PostCount { get; }

	public CategoryInfoViewModel(int id, string name, int postCount)
	{
	    Id = id;
	    Name = name;
	    PostCount = postCount;
	}
    }
}
