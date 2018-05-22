namespace Forum.App.Services
{
    using System;
    using System.Linq;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Contracts.ServiceContracts;
    using Forum.Data;
    using Forum.DataModels;

    public class UserService : IUserService
    {
	private const int MinUsernameLength = 3;
	private const int MinPasswordLength = 3;

	private ForumData forumData;
	private ISession session;

	public UserService(ForumData forumData, ISession session)
	{
	    this.forumData = forumData;
	    this.session = session;
	}

	public User GetUserById(int userId)
	{
	    User user = forumData.Users.SingleOrDefault(u => u.Id == userId);
	    return user;
	}

	public string GetUserName(int userId)
	{
	    string username = forumData.Users.SingleOrDefault(u => u.Id == userId).Username;
	    return username;
	}

	public void TrySignUpUser(string username, string password)
	{
	    bool validUsername = !String.IsNullOrWhiteSpace(username)
		&& username.Length > MinUsernameLength;
	    bool validPassword = !String.IsNullOrWhiteSpace(password)
		&& password.Length > MinPasswordLength;
	    if (!validUsername || !validPassword)
		throw new ArgumentException("Username and password must be longer than 3 symbols!");
	    bool userAlreadyExists = forumData.Users.Any(u => u.Username == username);
	    if (userAlreadyExists) throw new InvalidOperationException("Username taken!");
	    int userId = forumData.Users.LastOrDefault()?.Id + 1 ?? 1;
	    User user = new User(userId, username, password);
	    forumData.Users.Add(user);
	    forumData.SaveChanges();
	    TryLogInUser(username, password);
	}

	public void TryLogInUser(string username, string password)
	{
	    if (String.IsNullOrWhiteSpace(username))
		throw new ArgumentException("Please enter a username!");
	    if (String.IsNullOrWhiteSpace(password))
		throw new ArgumentException("Please enter a password!");
	    User user = forumData.Users.FirstOrDefault(
		u => u.Username == username && u.Password == password);
	    if (user == null) throw new ArgumentException("Invalid credentials!");
	    session.Reset();
	    session.LogIn(user);
	}

	public void LogOutUser()
	{
	    session.Reset();
	    session.LogOut();
	}
    }
}
