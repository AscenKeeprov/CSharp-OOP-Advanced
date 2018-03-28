using System;

namespace Twouple
{
    class Program
    {
        static void Main()
        {
	    string input = Console.ReadLine();
	    string[] parameters = input.Split();
	    string name = $"{parameters[0]} {parameters[1]}";
	    string address = parameters[2];
	    Tuple<string, string> tuple1 = new Tuple<string, string>(name, address);
	    Console.WriteLine(tuple1);
	    input = Console.ReadLine();
	    parameters = input.Split();
	    name = parameters[0];
	    int liters = int.Parse(parameters[1]);
	    Tuple<string, int> tuple2 = new Tuple<string, int>(name, liters);
	    Console.WriteLine(tuple2);
	    input = Console.ReadLine();
	    parameters = input.Split();
	    int num1 = int.Parse(parameters[0]);
	    double num2 = double.Parse(parameters[1]);
	    Tuple<int, double> tuple3 = new Tuple<int, double>(num1, num2);
	    Console.WriteLine(tuple3);
	}
    }
}
