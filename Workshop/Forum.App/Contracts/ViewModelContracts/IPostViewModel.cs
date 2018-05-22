namespace Forum.App.Contracts.ViewModelContracts
{
    public interface IPostViewModel : IContentViewModel
    {
	string Title { get; }
	string Author { get; }
	IReplyViewModel[] Replies { get; }
    }
}
