using System;

namespace Threeuple
{
    class Program
    {
        static void Main()
        {
	    string input = Console.ReadLine();
	    string[] parameters = input.Split();
	    string name = $"{parameters[0]} {parameters[1]}";
	    string address = parameters[2];
	    string town = parameters[3];
	    var tuple1 = new Tuple<string, string, string>(name, address,town);
	    Console.WriteLine(tuple1);
	    input = Console.ReadLine();
	    parameters = input.Split();
	    name = parameters[0];
	    int liters = int.Parse(parameters[1]);
	    bool isDrunk = parameters[2].ToUpper() == "DRUNK" ? true : false;
	    var tuple2 = new Tuple<string, int, bool>(name, liters, isDrunk);
	    Console.WriteLine(tuple2);
	    input = Console.ReadLine();
	    parameters = input.Split();
	    name = parameters[0];
	    double bankBalance = double.Parse(parameters[1]);
	    string bankName = parameters[2];
	    var tuple3 = new Tuple<string, double, string>(name, bankBalance, bankName);
	    Console.WriteLine(tuple3);
	}
    }
}
