namespace Forum.App.Contracts.ServiceContracts
{
    using DataModels;

    public interface IUserService
    {
	string GetUserName(int userId);
	User GetUserById(int userId);
	void TrySignUpUser(string username, string password);
	void TryLogInUser(string username, string password);
	void LogOutUser();
    }
}
