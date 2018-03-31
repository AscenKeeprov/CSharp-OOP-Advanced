using System.Linq;
using System.Reflection;

public class CommandInterpreter
{
    [Inject("Repository")]
    private IRepository repository;
    [Inject("UnitFactory")]
    private IUnitFactory unitFactory;
    private CommandFactory commandFactory;

    public CommandInterpreter(IRepository repository, IUnitFactory unitFactory)
    {
	this.repository = repository;
	this.unitFactory = unitFactory;
	commandFactory = new CommandFactory();
    }

    public string InterpretCommand(string[] input)
    {
	IExecutable command = commandFactory.CreateCommand(input);
	InjectDependencies(command);
	string output = command.Execute();
	return output;
    }

    private void InjectDependencies(IExecutable command)
    {
	FieldInfo[] fieldsToInject = command.GetType()
	    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
	    .Where(f => f.GetCustomAttribute(typeof(InjectAttribute)) != null).ToArray();
	FieldInfo[] injectionFields = GetType()
	    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
	    .Where(f => f.GetCustomAttribute(typeof(InjectAttribute)) != null).ToArray();
	foreach (FieldInfo field in fieldsToInject)
	{
	    if (injectionFields.Any(f => f.FieldType == field.FieldType))
	    {
		var valueToInject = injectionFields
		    .SingleOrDefault(f => f.FieldType == field.FieldType).GetValue(this);
		field.SetValue(command, valueToInject);
	    }
	}
    }
}
