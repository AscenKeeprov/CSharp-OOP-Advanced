using System;
using System.Collections.Generic;
using System.Linq;

public class Company : ICompany
{
    public string Name { get; }
    public ICollection<IJob> Jobs { get; private set; }
    public ICollection<IEmployee> Employees { get; }

    public Company(string name)
    {
	Name = name;
	Jobs = new List<IJob>();
	Employees = new List<IEmployee>();
    }

    public void BeginOperations()
    {
	string operation;
	while (!(operation = Console.ReadLine()).Equals("End"))
	{
	    string[] operationParameters = operation.Split();
	    try
	    {
		switch (operationParameters[0])
		{
		    case "StandardEmployee":
		    case "PartTimeEmployee":
			HireEmployee(operationParameters);
			break;
		    case "Job":
			string[] jobInfo = operationParameters.Skip(1).ToArray();
			OpenJob(jobInfo);
			break;
		    case "Pass":
			UpdateJobsStatus();
			break;
		    case "Status":
			PrintJobsStatus();
			break;
		}
	    }
	    catch (Exception exception)
	    {
		Console.WriteLine(exception.Message);
	    }
	}
    }

    public void HireEmployee(string[] employeeInfo)
    {
	string employeeName = employeeInfo[1];
	IEmployee employee = null;
	if (employeeInfo[0].Equals("StandardEmployee"))
	    employee = new StandardEmployee(employeeName);
	else if (employeeInfo[0].Equals("PartTimeEmployee"))
	    employee = new PartTimeEmployee(employeeName);
	Employees.Add(employee);
    }

    public void OpenJob(string[] jobInfo)
    {
	string jobName = jobInfo[0];
	int jobLength = int.Parse(jobInfo[1]);
	string employeeName = jobInfo[2];
	IEmployee employee = Employees
	    .SingleOrDefault(e => e.Name == employeeName);
	IJob job = new Job(jobName, jobLength, employee);
	Jobs.Add(job);
	job.JobDone += CloseJob;
    }

    public void CloseJob(IJob job)
    {
	job.EmployeeAssigned = null;
    }

    public void UpdateJobsStatus()
    {
	foreach (IJob job in Jobs)
	    job.UpdateStatus();
	Jobs = Jobs.Where(j => j.EmployeeAssigned != null).ToList();
    }

    public void PrintJobsStatus()
    {
	foreach (IJob job in Jobs)
	    job.PrintStatus();
    }
}
