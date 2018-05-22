namespace Forum.App.Models
{
    using System.Collections.Generic;
    using DataModels;
    using Forum.App.Contracts.ModelContracts;

    public class Session : ISession
    {
	private User user;
	private Stack<IMenu> history;

	public string Username => user?.Username;
	public int UserId => user?.Id ?? default(int);
	public bool IsLoggedIn => user != null;
	public IMenu CurrentMenu => history.Peek();

	public Session()
	{
	    history = new Stack<IMenu>();
	}

	public void LogIn(User user)
	{
	    this.user = user;
	}

	public void LogOut()
	{
	    user = null;
	}

	public bool PushView(IMenu view)
	{
	    if (history.Count == 0 || history.Peek() != view)
	    {
		history.Push(view);
		return true;
	    }
	    return false;
	}

	public IMenu Back()
	{
	    if (history.Count > 1) history.Pop();
	    IMenu previousMenu = history.Peek();
	    previousMenu.Open();
	    return previousMenu;
	}

	public void Reset()
	{
	    history = new Stack<IMenu>();
	}
    }
}
