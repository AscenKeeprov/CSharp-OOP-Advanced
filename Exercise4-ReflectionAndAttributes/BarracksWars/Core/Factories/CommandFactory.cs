using System;
using System.Linq;
using System.Reflection;

public class CommandFactory
{
    Type[] validCommandTypes => Assembly.GetExecutingAssembly().GetTypes()
	    .Where(t => t.BaseType == typeof(Command)).ToArray();

    public IExecutable CreateCommand(string[] input)
    {
	string commandName = input[0];
	Type commandType = validCommandTypes.SingleOrDefault(
	    c => c.Name.ToUpper().Contains(commandName.ToUpper()));
	if (commandType == null)
	    throw new InvalidOperationException("Invalid command!");
	string[] commandArgs = input.Skip(1).ToArray();
	IExecutable command = (IExecutable)Activator
	    .CreateInstance(commandType, new object[] { commandArgs });
	return command;
    }
}
