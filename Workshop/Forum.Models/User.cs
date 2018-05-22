namespace Forum.DataModels
{
    using System.Collections.Generic;

    public class User
    {
	public int Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public List<int> Posts { get; set; } = new List<int>();

	public User()
	{
	    Posts = new List<int>();
	}

	public User(int id, string username, string password) : this()
	{
	    Id = id;
	    Username = username;
	    Password = password;
	}

	public User(int id, string username, string password, IEnumerable<int> posts)
	    : this(id, username, password)
	{
	    Posts.AddRange(new List<int>(posts));
	}
    }
}
