namespace Forum.App.Commands
{
    using System;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.ModelContracts;

    public class ViewCategoryMenuCommand : Command
    {
	public ViewCategoryMenuCommand(IMenuFactory menuFactory) : base(menuFactory) { }

	public override IMenu Execute(params string[] args)
	{
	    int categoryId = int.Parse(args[0]);
	    string commandName = GetType().Name;
	    string menuName = commandName.Replace(GetType().BaseType.Name, String.Empty);
	    IIdHoldingMenu menu = (IIdHoldingMenu)menuFactory.CreateMenu(menuName);
	    menu.SetId(categoryId);
	    return menu;
	}
    }
}
