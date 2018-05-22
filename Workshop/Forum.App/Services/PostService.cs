namespace Forum.App.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Forum.App.Contracts.ServiceContracts;
    using Forum.App.Contracts.ViewModelContracts;
    using Forum.App.ViewModels;
    using Forum.Data;
    using Forum.DataModels;

    public class PostService : IPostService
    {
	private ForumData forumData;
	private IUserService userService;

	public PostService(ForumData forumData, IUserService userService)
	{
	    this.forumData = forumData;
	    this.userService = userService;
	}

	public int AddPost(int userId, string postTitle, string postCategory, string postContent)
	{
	    bool missingTitle = String.IsNullOrWhiteSpace(postTitle);
	    bool missingCategory = String.IsNullOrWhiteSpace(postCategory);
	    bool missingContent = String.IsNullOrWhiteSpace(postContent);
	    if (missingTitle || missingCategory || missingContent)
		throw new ArgumentException("All fields must be filled!");
	    Category category = EnsureCategory(postCategory);
	    int postId = forumData.Posts.LastOrDefault()?.Id + 1 ?? 1;
	    User author = userService.GetUserById(userId);
	    Post post = new Post(postId, postTitle, postContent, category.Id, author.Id);
	    forumData.Posts.Add(post);
	    category.Posts.Add(post.Id);
	    author.Posts.Add(post.Id);
	    forumData.SaveChanges();
	    return post.Id;
	}

	private Category EnsureCategory(string categoryName)
	{
	    Category category = forumData.Categories
		.FirstOrDefault(c => c.Name == categoryName);
	    if (category == null)
	    {
		int categoryId = forumData.Categories.LastOrDefault()?.Id + 1 ?? 1;
		category = new Category(categoryId, categoryName);
		forumData.Categories.Add(category);
		forumData.SaveChanges();
	    }
	    return category;
	}

	public void AddReplyToPost(int postId, string replyContent, int userId)
	{
	    if (String.IsNullOrWhiteSpace(replyContent))
		throw new ArgumentException("Cannot add an empty reply!");
	    Post post = forumData.Posts.SingleOrDefault(p => p.Id == postId);
	    int replyId = forumData.Replies.LastOrDefault()?.Id + 1 ?? 1;
	    Reply reply = new Reply(replyId, replyContent, userId, postId);
	    forumData.Replies.Add(reply);
	    post.Replies.Add(replyId);
	    forumData.SaveChanges();
	}

	public IEnumerable<ICategoryInfoViewModel> GetAllCategories()
	{
	    IEnumerable<ICategoryInfoViewModel> categories = forumData.Categories
		.Select(c => new CategoryInfoViewModel(c.Id, c.Name, c.Posts.Count));
	    return categories;
	}

	public string GetCategoryName(int categoryId)
	{
	    string categoryName = forumData.Categories
		.FirstOrDefault(c => c.Id == categoryId).Name;
	    if (String.IsNullOrEmpty(categoryName))
		throw new ArgumentNullException($"Category with id {categoryId} not found!");
	    return categoryName;
	}

	public IEnumerable<IPostInfoViewModel> GetCategoryPostsInfo(int categoryId)
	{
	    IEnumerable<IPostInfoViewModel> categoryPosts = forumData.Posts
		.Where(p => p.CategoryId == categoryId)
		.Select(p => new PostInfoViewModel(p.Id, p.Title, p.Replies.Count));
	    return categoryPosts;
	}

	public IPostViewModel GetPostViewModel(int postId)
	{
	    Post post = forumData.Posts.SingleOrDefault(p => p.Id == postId);
	    string postAuthor = userService.GetUserName(post.AuthorId);
	    IEnumerable<IReplyViewModel> postReplies = GetPostReplies(postId);
	    IPostViewModel postViewModel = new PostViewModel(
		post.Title, postAuthor, post.Content, postReplies);
	    return postViewModel;
	}

	private IEnumerable<IReplyViewModel> GetPostReplies(int postId)
	{
	    IEnumerable<IReplyViewModel> replies = forumData.Replies
		.Where(r => r.PostId == postId)
		.Select(r => new ReplyViewModel(userService.GetUserName(r.AuthorId), r.Content));
	    return replies;
	}
    }
}
