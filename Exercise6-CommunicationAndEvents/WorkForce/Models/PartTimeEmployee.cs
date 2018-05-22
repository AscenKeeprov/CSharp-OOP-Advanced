public class PartTimeEmployee : Employee
{
    private const int WorkHoursPerWeek_PartTimeEmployee = 20;

    public PartTimeEmployee(string name)
	: base(name, WorkHoursPerWeek_PartTimeEmployee) { }
}
