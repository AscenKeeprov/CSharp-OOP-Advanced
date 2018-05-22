public class StandardEmployee : Employee
{
    private const int WorkHoursPerWeek_StandardEmployee = 40;

    public StandardEmployee(string name)
	: base(name, WorkHoursPerWeek_StandardEmployee) { }
}
