namespace Forum.App.Contracts.FactoryContracts
{
    using Forum.App.Contracts.ModelContracts;

    public interface IMenuFactory
    {
	IMenu CreateMenu(string menuName);
    }
}
