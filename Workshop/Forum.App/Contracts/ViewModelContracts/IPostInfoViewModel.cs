namespace Forum.App.Contracts.ViewModelContracts
{
    public interface IPostInfoViewModel
    {
	int Id { get; }
	string Title { get; }
	int ReplyCount { get; }
    }
}
