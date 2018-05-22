namespace Forum.DataModels
{
    public class Reply
    {
	public int Id { get; set; }
	public string Content { get; set; }
	public int AuthorId { get; set; }
	public int PostId { get; set; }

	public Reply(string content)
	{
	    Content = content;
	}

	public Reply(string content, int authorId) : this(content)
	{
	    AuthorId = authorId;
	}

	public Reply(int id, string content, int authorId, int postId)
	    : this(content, authorId)
	{
	    Id = id;
	    PostId = postId;
	}
    }
}
