using System;
using Microsoft.Extensions.DependencyInjection;

public class Server: IServiceProvider
{
    private IServiceCollection services;
    private IServiceProvider ServiceProvider;

    internal void InitializeServices()
    {
	services = new ServiceCollection();
	services.AddSingleton<IUserInterface, UserInterface>();
	services.AddSingleton<IFileSystemManager, FSManager>();
	services.AddSingleton<IInputOutputManager, IOManager>();
	services.AddSingleton<ICommandFactory, CommandFactory>();
	services.AddSingleton<ICommandInterpreter, CommandInterpreter>();
	services.AddSingleton<ICommandCache, CommandCache>();
	services.AddSingleton<IRepository, Repository>();
	services.AddScoped<IFilter, Filter>();
	services.AddScoped<ISorter, Sorter>();
	services.AddScoped<ITester, Tester>();
	ServiceProvider = services.BuildServiceProvider();
    }

    public object GetService(Type serviceType)
    {
	return ServiceProvider.GetService(serviceType);
    }
}
