using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInversion
{
    public class StartUp
    {
	public static void Main()
	{
	    IServiceProvider strategyProvider = InitializeStrategies();
	    PrimitiveCalculator calculator = new PrimitiveCalculator(strategyProvider);
	    string input;
	    while (!(input = Console.ReadLine()).ToUpper().Equals("END"))
	    {
		string[] operationArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		if (int.TryParse(operationArgs[0], out int number1) &&
		    int.TryParse(operationArgs[1], out int number2))
		{
		    int calculationResult = calculator.PerformCalculation(number1, number2);
		    Console.WriteLine(calculationResult);
		}
		else if (operationArgs[0].ToUpper().Equals("MODE"))
		{
		    if (Char.TryParse(operationArgs[1], out char @operator))
			calculator.ChangeStrategy(@operator);
		}
	    }
	}

	private static IServiceProvider InitializeStrategies()
	{
	    IServiceCollection strategies = new ServiceCollection();
	    strategies.AddSingleton<AdditionStrategy>();
	    strategies.AddSingleton<SubtractionStrategy>();
	    strategies.AddSingleton<MultiplicationStrategy>();
	    strategies.AddSingleton<DivisionStrategy>();
	    IServiceProvider strategyProvider = strategies.BuildServiceProvider();
	    return strategyProvider;
	}
    }
}
