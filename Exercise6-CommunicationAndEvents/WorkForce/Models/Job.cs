using System;

public class Job : IJob
{
    public event JobDoneEventHandler JobDone;

    public string Name { get; }
    public int HoursOfWorkRequired { get; private set; }
    public IEmployee EmployeeAssigned { get; set; }

    public Job(string name, int hoursOfWorkRequired, IEmployee employee)
    {
	Name = name;
	HoursOfWorkRequired = hoursOfWorkRequired;
	EmployeeAssigned = employee;
    }

    public void UpdateStatus()
    {
	HoursOfWorkRequired -= EmployeeAssigned.WorkHoursPerWeek;
	if (HoursOfWorkRequired <= 0)
	{
	    Console.WriteLine($"{GetType().Name} {Name} done!");
	    if (JobDone != null) JobDone.Invoke(this);
	}
    }

    public void PrintStatus()
    {
	Console.WriteLine($"{GetType().Name}: {Name} Hours Remaining: {HoursOfWorkRequired}");
    }
}
