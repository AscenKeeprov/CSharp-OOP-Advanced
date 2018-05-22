using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInversion
{
    public class PrimitiveCalculator
    {
	private IServiceProvider strategyProvider;
	private ICalculationStrategy currentStrategy;

	public PrimitiveCalculator(IServiceProvider strategyProvider)
	{
	    this.strategyProvider = strategyProvider;
	    currentStrategy = strategyProvider.GetService<AdditionStrategy>();
	}

	public void ChangeStrategy(char @operator)
	{
	    switch (@operator)
	    {
		case '+':
		    currentStrategy = strategyProvider.GetService<AdditionStrategy>();
		    break;
		case '-':
		    currentStrategy = strategyProvider.GetService<SubtractionStrategy>();
		    break;
		case '*':
		    currentStrategy = strategyProvider.GetService<MultiplicationStrategy>();
		    break;
		case '/':
		    currentStrategy = strategyProvider.GetService<DivisionStrategy>();
		    break;
	    }
	}

	public int PerformCalculation(int operand1, int operand2)
	{
	    int calculationResult = currentStrategy.Calculate(operand1, operand2);
	    return calculationResult;
	}
    }
}
