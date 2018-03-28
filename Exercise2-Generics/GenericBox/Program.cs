using System;

namespace GenericBox
{
    class Program
    {
	static void Main()
	{
	    int boxesCount = int.Parse(Console.ReadLine());
	    for (int b = 1; b <= boxesCount; b++)
	    {
		var contents = Console.ReadLine();
		IBox box = FillBox(contents);
		Console.WriteLine(box);
	    }
	}

	static IBox FillBox(string contents)
	{
	    IBox box = null;
	    if (int.TryParse(contents, out int num))
		box = new Box<int>(num);
	    else box = new Box<string>(contents);
	    return box;
	}
    }
}
