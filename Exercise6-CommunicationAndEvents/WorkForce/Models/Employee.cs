public abstract class Employee : IEmployee
{
    public string Name { get; }
    public int WorkHoursPerWeek { get; }

    public Employee(string name, int workHoursPerWeek)
    {
	Name = name;
	WorkHoursPerWeek = workHoursPerWeek;
    }
}
