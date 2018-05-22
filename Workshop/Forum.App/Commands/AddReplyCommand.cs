namespace Forum.App.Commands
{
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ServiceContracts;
    using Forum.App.Menus;

    public class AddReplyCommand : Command
    {
	public AddReplyCommand(ISession session, IPostService postService, ICommandFactory commandFactory)
	    : base(session, postService, commandFactory) { }

	public override IMenu Execute(params string[] args)
	{
	    int userId = session.UserId;
	    int postId = int.Parse(args[0]);
	    string replyContents = args[1];
	    postService.AddReplyToPost(postId, replyContents, userId);
	    session.Back();
	    session.Back();
	    ICommand command = commandFactory.CreateCommand(typeof(ViewPostMenu).Name);
	    IMenu menu = command.Execute(postId.ToString());
	    return menu;
	}
    }
}
