using System;
using Microsoft.Extensions.DependencyInjection;

namespace InfernoInfinity
{
    public class Story
    {
	static void Main()
	{
	    IServiceProvider serviceProvider = InitializeServices();
	    TaskMaster taskMaster = new TaskMaster(serviceProvider);
	    Camp camp = new Camp(taskMaster);
	    camp.BeginWork();
	}

	private static IServiceProvider InitializeServices()
	{
	    IServiceCollection services = new ServiceCollection();
	    services.AddSingleton<IArmoury, Armoury>();
	    services.AddTransient<IBlacksmith, Blacksmith>();
	    services.AddTransient<IJeweller, Jeweller>();
	    IServiceProvider serviceProvider = services.BuildServiceProvider();
	    return serviceProvider;
	}
    }
}
