using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>Creates new command instances.</summary>
public class CommandFactory : ICommandFactory
{
    Assembly Assembly => Assembly.GetExecutingAssembly();
    Type[] CommandTypes => Assembly.GetTypes().Where(type
	=> type.BaseType == typeof(Command)).ToArray();
    Type[] AliasedCommands => CommandTypes.Where(type
	=> type.CustomAttributes.Any(a => a.AttributeType
	== typeof(AliasAttribute))).ToArray();
    private IServiceProvider serviceProvider;

    public CommandFactory(IServiceProvider serviceProvider)
    {
	this.serviceProvider = serviceProvider;
    }

    public ICommand Create(string[] commandParameters)
    {
	if (commandParameters.Length == 0) throw new MissingCommandException();
	string commandAlias = commandParameters[0].ToUpper();
	Type commandType = AliasedCommands.FirstOrDefault(aliasedCommand
	    => aliasedCommand.CustomAttributes.Any(customAttribute
	    => customAttribute.ConstructorArguments.Any(alias
	    => alias.Value.Equals(commandAlias))));
	if (commandType == null) throw new InvalidCommandException(commandAlias);
	if (!typeof(IExecutable).IsAssignableFrom(commandType))
	    throw new NonExecutableCommandException(commandAlias);
	ConstructorInfo commandConstructor = commandType.GetConstructors()[0];
	ParameterInfo[] constructorParameters = commandConstructor.GetParameters();
	IList<object> commandDependencies = new List<object> { commandParameters };
	for (int i = 1; i < constructorParameters.Length; i++)
	{
	    Type serviceType = constructorParameters[i].ParameterType;
	    object service = serviceProvider.GetService(serviceType);
	    commandDependencies.Add(service);
	}
	ICommand command = (ICommand)Activator
	    .CreateInstance(commandType, commandDependencies.ToArray());
	return command;
    }
}
