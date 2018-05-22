namespace Forum.Data
{
    using System.Collections.Generic;
    using Forum.DataModels;

    public class ForumData
    {
	public List<Category> Categories { get; set; }
	public List<User> Users { get; set; }
	public List<Post> Posts { get; set; }
	public List<Reply> Replies { get; set; }

	public ForumData()
	{
	    Categories = DataMapper.LoadCategories();
	    Users = DataMapper.LoadUsers();
	    Posts = DataMapper.LoadPosts();
	    Replies = DataMapper.LoadReplies();
	}

	public void SaveChanges()
	{
	    DataMapper.SaveCategories(this.Categories);
	    DataMapper.SaveUsers(this.Users);
	    DataMapper.SavePosts(this.Posts);
	    DataMapper.SaveReplies(this.Replies);
	}
    }
}
