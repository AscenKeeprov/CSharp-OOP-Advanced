namespace DependencyInversion
{
    public class AdditionStrategy : ICalculationStrategy
    {
	public int Calculate(int operand1, int operand2)
	{
	    return operand1 + operand2;
	}
    }
}
