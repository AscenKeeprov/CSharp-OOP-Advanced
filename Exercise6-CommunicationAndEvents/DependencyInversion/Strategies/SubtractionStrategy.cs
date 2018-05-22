namespace DependencyInversion
{
    public class SubtractionStrategy : ICalculationStrategy
    {
	public int Calculate(int operand1, int operand2)
	{
	    return operand1 - operand2;
	}
    }
}
