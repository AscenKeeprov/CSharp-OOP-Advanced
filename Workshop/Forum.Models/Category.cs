namespace Forum.DataModels
{
    using System.Collections.Generic;

    public class Category
    {
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<int> Posts { get; set; } = new List<int>();

	public Category()
	{
	    Posts = new List<int>();
	}

	public Category(string name) : this()
	{
	    Name = name;
	}

	public Category(int id, string name) : this(name)
	{
	    Id = id;
	    Name = name;
	}

	public Category(int id, string name, IEnumerable<int> posts)
	    : this(id, name)
	{
	    Posts = new List<int>(posts);
	}
    }
}
