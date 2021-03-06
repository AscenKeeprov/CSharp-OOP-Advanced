﻿namespace Forum.App.Commands
{
    using System;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.MenuContracts;
    using Forum.App.Contracts.ModelContracts;

    public class AddReplyMenuCommand : Command
    {
	public AddReplyMenuCommand(IMenuFactory menuFactory) : base(menuFactory) { }

	public override IMenu Execute(params string[] args)
	{
	    int postId = int.Parse(args[0]);
	    string commandName = GetType().Name;
	    string menuName = commandName.Replace(GetType().BaseType.Name, String.Empty);
	    IIdHoldingMenu menu = (IIdHoldingMenu)menuFactory.CreateMenu(menuName);
	    menu.SetId(postId);
	    return menu;
	}
    }
}
