namespace Forum.App.Commands
{
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ServiceContracts;
    using Forum.App.Menus;

    public class AddPostCommand : Command
    {
	public AddPostCommand(ISession session, IPostService postService, ICommandFactory commandFactory)
	    : base(session, postService, commandFactory) { }

	public override IMenu Execute(params string[] args)
	{
	    int userId = session.UserId;
	    string postTitle = args[0];
	    string postCategory = args[1];
	    string postContent = args[2];
	    int postId = postService.AddPost(userId, postTitle, postCategory, postContent);
	    session.Back();
	    ICommand command = commandFactory.CreateCommand(typeof(ViewPostMenu).Name);
	    IMenu menu = command.Execute(postId.ToString());
	    return menu;
	}
    }
}
