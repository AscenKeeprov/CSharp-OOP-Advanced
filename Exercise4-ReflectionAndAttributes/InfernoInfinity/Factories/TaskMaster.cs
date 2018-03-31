using System;
using System.Linq;
using System.Reflection;

public class TaskMaster : ITaskMaster
{
    private Type[] taskTypes = Assembly.GetExecutingAssembly()
	.GetTypes().Where(t => t.BaseType == typeof(Task)).ToArray();
    private IServiceProvider serviceProvider;

    public TaskMaster(IServiceProvider serviceProvider)
    {
	this.serviceProvider = serviceProvider;
    }

    public IExecutable AssignTask(string[] taskInfo)
    {
	string action = taskInfo[0].ToUpper();
	Type taskType = taskTypes.SingleOrDefault(t => t.Name.ToUpper() == $"{action}TASK");
	if (taskType == null) throw new ArgumentException("Invalid task!");
	if (!typeof(IExecutable).IsAssignableFrom(taskType))
	    throw new InvalidOperationException("Tasks of this type cannot be executed!");
	BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
	FieldInfo[] fieldsToInject = taskType
	    .GetFields(bindingFlags).Where(f => f.CustomAttributes
	    .Any(ca => ca.AttributeType == typeof(InjectAttribute))).ToArray();
	object[] servicesToInject = fieldsToInject.Select(
	    f => serviceProvider.GetService(f.FieldType)).ToArray();
	object[] taskParameters = new object[] { taskInfo.Skip(1).ToArray() };
	taskParameters = taskParameters.Concat(servicesToInject).ToArray();
	IExecutable task = (IExecutable)Activator.CreateInstance(taskType, taskParameters);
	return task;
    }
}
