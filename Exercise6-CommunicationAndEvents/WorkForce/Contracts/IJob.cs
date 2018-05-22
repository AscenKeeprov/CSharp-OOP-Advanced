public delegate void JobDoneEventHandler(IJob job);

public interface IJob
{
    event JobDoneEventHandler JobDone;

    string Name { get; }
    int HoursOfWorkRequired { get; }
    IEmployee EmployeeAssigned { get; set; }

    void UpdateStatus();
    void PrintStatus();
}
