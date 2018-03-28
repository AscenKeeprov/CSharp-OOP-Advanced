using System;
using System.Linq;

namespace Froggy
{
    class Program
    {
        static void Main()
        {
	    int[] stones = Console.ReadLine()
		.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    Lake lake = new Lake(stones);
	    Frog frog = new Frog();
	    Console.WriteLine(frog.Cross(lake));
        }
    }
}
