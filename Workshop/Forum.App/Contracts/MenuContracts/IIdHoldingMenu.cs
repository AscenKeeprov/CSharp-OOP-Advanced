namespace Forum.App.Contracts.MenuContracts
{
    using Forum.App.Contracts.ModelContracts;

    public interface IIdHoldingMenu : IMenu
    {
	void SetId(int id);
    }
}
