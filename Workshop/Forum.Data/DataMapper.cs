namespace Forum.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Forum.DataModels;

    internal static class DataMapper
    {
	private const string DATA_PATH = "../../../../data/";
	private const string CONFIG_PATH = "config.ini";
	private const string DEFAULT_CONFIG = "users=users.csv\r\ncategories=categories.csv\r\nposts=posts.csv\r\nreplies=replies.csv";

	private static readonly Dictionary<string, string> config;

	static DataMapper()
	{
	    Directory.CreateDirectory(DATA_PATH);
	    config = LoadConfig(DATA_PATH + CONFIG_PATH);
	}

	private static Dictionary<string, string> LoadConfig(string configFilePath)
	{
	    EnsureConfigFile(configFilePath);
	    string[] configFileContents = ReadFile(configFilePath);
	    Dictionary<string, string> config = configFileContents
		.Select(lines => lines.Split('=')).ToDictionary(line =>
		/*setting name*/ line[0],
		/*setting value*/ line => DATA_PATH + line[1]);
	    return config;
	}

	private static void EnsureConfigFile(string configFilePath)
	{
	    if (!File.Exists(configFilePath))
		File.WriteAllText(configFilePath, DEFAULT_CONFIG);
	}

	private static string[] ReadFile(string filePath)
	{
	    EnsureFile(filePath);
	    string[] fileContents = File.ReadAllLines(filePath);
	    return fileContents;
	}

	private static void EnsureFile(string filePath)
	{
	    if (!File.Exists(filePath))
		File.Create(filePath).Close();
	}

	private static void WriteFile(string filePath, string[] fileContents)
	{
	    File.WriteAllLines(filePath, fileContents);
	}

	public static List<Category> LoadCategories()
	{
	    List<Category> categories = new List<Category>();
	    string[] categoriesData = ReadFile(config["categories"]);
	    foreach (string entry in categoriesData)
	    {
		string[] categoryInfo = entry.Split(';', StringSplitOptions.RemoveEmptyEntries);
		int id = int.Parse(categoryInfo[0]);
		string name = categoryInfo[1];
		Category category = new Category(id, name);
		if (categoryInfo.Length == 3)
		{
		    List<int> posts = categoryInfo[2]
			.Split(',', StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse).ToList();
		    category.Posts = posts;
		}
		categories.Add(category);
	    }
	    return categories;
	}

	public static void SaveCategories(List<Category> categories)
	{
	    string[] categoriesData = new string[categories.Count];
	    for (int c = 0; c < categoriesData.Length; c++)
	    {
		Category category = categories[c];
		string categoryInfo = String.Format("{0};{1};{2}",
		    category.Id, category.Name,
		    String.Join(",", category.Posts));
		categoriesData[c] = categoryInfo;
	    }
	    WriteFile(config["categories"], categoriesData);
	}

	public static List<User> LoadUsers()
	{
	    List<User> users = new List<User>();
	    string[] usersData = ReadFile(config["users"]);
	    foreach (string entry in usersData)
	    {
		string[] userInfo = entry.Split(';', StringSplitOptions.RemoveEmptyEntries);
		int id = int.Parse(userInfo[0]);
		string username = userInfo[1];
		string password = userInfo[2];
		User user = new User(id, username, password);
		if (userInfo.Length == 4)
		{
		    List<int> posts = userInfo[3]
			.Split(',', StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse).ToList();
		    user.Posts = posts;
		}
		users.Add(user);
	    }
	    return users;
	}

	public static void SaveUsers(List<User> users)
	{
	    string[] usersData = new string[users.Count];
	    for (int u = 0; u < usersData.Length; u++)
	    {
		User user = users[u];
		string userInfo = String.Format("{0};{1};{2};{3}",
		    user.Id, user.Username, user.Password,
		    String.Join(",", user.Posts));
		usersData[u] = userInfo;
	    }
	    WriteFile(config["users"], usersData);
	}

	public static List<Post> LoadPosts()
	{
	    List<Post> posts = new List<Post>();
	    string[] postsData = ReadFile(config["posts"]);
	    foreach (string entry in postsData)
	    {
		string[] postInfo = entry.Split(';', StringSplitOptions.RemoveEmptyEntries);
		int id = int.Parse(postInfo[0]);
		string title = postInfo[1];
		string content = postInfo[2];
		int categoryId = int.Parse(postInfo[3]);
		int authorId = int.Parse(postInfo[4]);
		Post post = new Post(id, title, content, categoryId, authorId);
		if (postInfo.Length == 6)
		{
		    List<int> replies = postInfo[5]
			.Split(',', StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse).ToList();
		    post.Replies = replies;
		}
		posts.Add(post);
	    }
	    return posts;
	}

	public static void SavePosts(List<Post> posts)
	{
	    string[] postsData = new string[posts.Count];
	    for (int p = 0; p < postsData.Length; p++)
	    {
		Post post = posts[p];
		string postInfo = String.Format(
		    "{0};{1};{2};{3};{4};{5}", post.Id,
		    post.Title, post.Content, post.CategoryId,
		    post.AuthorId, String.Join(",", post.Replies));
		postsData[p] = postInfo;
	    }
	    WriteFile(config["posts"], postsData);
	}

	public static List<Reply> LoadReplies()
	{
	    List<Reply> replies = new List<Reply>();
	    string[] repliesData = ReadFile(config["replies"]);
	    foreach (string entry in repliesData)
	    {
		string[] replyInfo = entry.Split(';', StringSplitOptions.RemoveEmptyEntries);
		int id = int.Parse(replyInfo[0]);
		string content = replyInfo[1];
		int authorId = int.Parse(replyInfo[2]);
		int postId = int.Parse(replyInfo[3]);
		Reply reply = new Reply(id, content, authorId, postId);
		replies.Add(reply);
	    }
	    return replies;
	}

	public static void SaveReplies(List<Reply> replies)
	{
	    string[] repliesData = new string[replies.Count];
	    for (int r = 0; r < repliesData.Length; r++)
	    {
		Reply reply = replies[r];
		string replyInfo = String.Format("{0};{1};{2};{3}",
		    reply.Id, reply.Content, reply.AuthorId, reply.PostId);
		repliesData[r] = replyInfo;
	    }
	    WriteFile(config["replies"], repliesData);
	}
    }
}
