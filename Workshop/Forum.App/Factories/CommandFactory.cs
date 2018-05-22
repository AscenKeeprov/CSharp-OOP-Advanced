namespace Forum.App.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Forum.App.Commands;
    using Forum.App.Contracts.FactoryContracts;
    using Forum.App.Contracts.ModelContracts;

    public class CommandFactory : ICommandFactory
    {
	Assembly Assembly => Assembly.GetExecutingAssembly();
	Type CommandBaseType => typeof(Command);
	Type[] CommandTypes => Assembly.GetTypes()
	    .Where(type => type.BaseType == CommandBaseType).ToArray();

	private IServiceProvider serviceProvider;

	public CommandFactory(IServiceProvider serviceProvider)
	{
	    this.serviceProvider = serviceProvider;
	}

	public ICommand CreateCommand(string commandName)
	{
	    Type commandType = CommandTypes.FirstOrDefault(
		t => t.Name.Equals(commandName + CommandBaseType.Name));
	    if (commandType == null) throw new InvalidOperationException("Command not found!");
	    if (!typeof(ICommand).IsAssignableFrom(commandType))
		throw new InvalidOperationException($"{commandType} is not a command!");
	    ConstructorInfo[] commandConstructors = commandType.GetConstructors();
	    ParameterInfo[] constructorParameters = commandConstructors.First().GetParameters();
	    object[] commandDependencies = new object[constructorParameters.Length];
	    for (int i = 0; i < commandDependencies.Length; i++)
	    {
		Type serviceType = constructorParameters[i].ParameterType;
		commandDependencies[i] = serviceProvider.GetService(serviceType);
	    }
	    ICommand command = (ICommand)Activator.CreateInstance(commandType, commandDependencies);
	    return command;
	}
    }
}
