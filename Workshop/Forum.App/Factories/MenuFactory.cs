namespace Forum.App.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;
    using Forum.App.Menus;

    public class MenuFactory : IMenuFactory
    {
	Assembly Assembly => Assembly.GetExecutingAssembly();
	Type[] MenuTypes => Assembly.GetTypes()
	    .Where(type => type.BaseType == typeof(Menu)).ToArray();

	private IServiceProvider serviceProvider;

	public MenuFactory(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public IMenu CreateMenu(string menuName)
	{
	    Type menuType = MenuTypes.SingleOrDefault(t => t.Name.Equals(menuName));
	    if (menuType == null) throw new InvalidOperationException("Menu not found!");
	    if (!typeof(IMenu).IsAssignableFrom(menuType))
		throw new InvalidOperationException($"{menuType} is not a menu!");
	    ConstructorInfo[] menuConstructors = menuType.GetConstructors();
	    ParameterInfo[] constructorParameters = menuConstructors.First().GetParameters();
	    object[] menuDependencies = new object[constructorParameters.Length];
	    for (int i = 0; i < menuDependencies.Length; i++)
	    {
		Type serviceType = constructorParameters[i].ParameterType;
		menuDependencies[i] = serviceProvider.GetService(serviceType);
	    }
	    IMenu menu = (IMenu)Activator.CreateInstance(menuType, menuDependencies);
	    return menu;
	}
    }
}
