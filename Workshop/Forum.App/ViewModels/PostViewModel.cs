namespace Forum.App.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Forum.App.Contracts.ViewModelContracts;

    public class PostViewModel : ContentViewModel, IPostViewModel
    {
	public string Title { get; }
	public string Author { get; }
	public IReplyViewModel[] Replies { get; }

	public PostViewModel(string title, string author, string content, IEnumerable<IReplyViewModel> replies)
	    : base(content)
	{
	    Title = title;
	    Author = author;
	    Replies = replies.ToArray();
	}
    }
}
