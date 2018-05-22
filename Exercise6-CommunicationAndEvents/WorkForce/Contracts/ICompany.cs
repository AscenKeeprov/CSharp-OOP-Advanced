using System.Collections.Generic;

public interface ICompany
{
    string Name { get; }
    ICollection<IJob> Jobs { get; }
    ICollection<IEmployee> Employees { get; }

    void BeginOperations();
    void HireEmployee(params string[] employeeInfo);
    void OpenJob(params string[] jobInfo);
    void CloseJob(IJob job);
    void UpdateJobsStatus();
    void PrintJobsStatus();
}
