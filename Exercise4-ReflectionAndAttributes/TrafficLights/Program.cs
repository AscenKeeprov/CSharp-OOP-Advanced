using System;

namespace TrafficLights
{
    class Program
    {
	static void Main()
	{
	    string[] lights = Console.ReadLine().Split();
	    TrafficLight[] trafficLights = new TrafficLight[lights.Length];
	    for (int l = 0; l < lights.Length; l++)
		trafficLights[l] = new TrafficLight(lights[l]);
	    int changesCount = int.Parse(Console.ReadLine());
	    for (int c = 1; c <= changesCount; c++)
	    {
		foreach (TrafficLight trafficLight in trafficLights)
		{
		    trafficLight.ChangeLight();
		    Console.Write($"{trafficLight} ");
		}
		Console.WriteLine();
	    }
	}
    }
}
