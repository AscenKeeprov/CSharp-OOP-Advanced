namespace Forum.DataModels
{
    using System.Collections.Generic;

    public class Post
    {
	public int Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public int CategoryId { get; set; }
	public int AuthorId { get; set; }
	public ICollection<int> Replies { get; set; } = new List<int>();

	public Post()
	{
	    Replies = new List<int>();
	}

	public Post(int id, string title, string content, int categoryId, int authorId) : this()
	{
	    Id = id;
	    Title = title;
	    Content = content;
	    CategoryId = categoryId;
	    AuthorId = authorId;
	}
    }
}
