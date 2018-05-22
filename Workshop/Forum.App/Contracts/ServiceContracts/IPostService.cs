namespace Forum.App.Contracts.ServiceContracts
{
    using System.Collections.Generic;
    using ViewModelContracts;

    public interface IPostService
    {
	string GetCategoryName(int categoryId);
	IEnumerable<ICategoryInfoViewModel> GetAllCategories();
	IEnumerable<IPostInfoViewModel> GetCategoryPostsInfo(int categoryId);
	IPostViewModel GetPostViewModel(int postId);
	int AddPost(int userId, string postTitle, string postCategory, string postContent);
	void AddReplyToPost(int postId, string replyContents, int userId);
    }
}
