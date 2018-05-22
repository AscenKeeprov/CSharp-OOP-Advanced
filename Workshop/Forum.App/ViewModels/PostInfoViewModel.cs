namespace Forum.App.ViewModels
{
    using Forum.App.Contracts.ViewModelContracts;

    public class PostInfoViewModel : IPostInfoViewModel
    {
	public int Id { get; }
	public string Title { get; }
	public int ReplyCount { get; }

	public PostInfoViewModel(int id, string title, int replyCount)
	{
	    Id = id;
	    Title = title;
	    ReplyCount = replyCount;
	}
    }
}
