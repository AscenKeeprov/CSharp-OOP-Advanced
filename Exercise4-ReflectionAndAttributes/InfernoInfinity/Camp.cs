using System;

public class Camp
{
    private ITaskMaster taskMaster;

    public Camp(ITaskMaster taskMaster)
    {
	this.taskMaster = taskMaster;
    }

    internal void BeginWork()
    {
	string work;
	while (!(work = Console.ReadLine()).ToUpper().Equals("END"))
	{
	    try
	    {
		string[] taskInfo = work.Split(';');
		IExecutable task = taskMaster.AssignTask(taskInfo);
		task.Execute();
	    }
	    catch (Exception exception)
	    {
		Console.WriteLine(exception.Message);
	    }
	}
    }
}
