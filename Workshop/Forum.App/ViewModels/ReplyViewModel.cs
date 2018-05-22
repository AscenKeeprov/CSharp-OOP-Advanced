namespace Forum.App.ViewModels
{
    using Forum.App.Contracts.ViewModelContracts;

    public class ReplyViewModel : ContentViewModel, IReplyViewModel
    {
	public string Author { get; }

	public ReplyViewModel(string author, string content) : base(content)
	{
	    Author = author;
	}
    }
}
