using System;
using System.Linq;
using System.Reflection;

namespace BlackBoxInteger
{
    public class Program
    {
        public static void Main()
        {
	    Type type = Type.GetType("BlackBoxInt");
	    ConstructorInfo constructor = type.GetConstructor(
		BindingFlags.Instance | BindingFlags.NonPublic,
		null, Type.EmptyTypes, null);
	    BlackBoxInt number = (BlackBoxInt)constructor.Invoke(null);
	    FieldInfo numberValue = type.GetFields(
		BindingFlags.Instance | BindingFlags.NonPublic)
		.SingleOrDefault(f => f.Name.ToLower().Contains("value"));
	    MethodInfo[] numberMethods = type.GetMethods(
		BindingFlags.Instance | BindingFlags.NonPublic);
	    string input;
	    while (!(input = Console.ReadLine()).Equals("END"))
	    {
		string command = input.Split('_')[0];
		int value = int.Parse(input.Split('_')[1]);
		MethodInfo method = numberMethods.SingleOrDefault(m => m.Name == command);
		if (method != null) method.Invoke(number, new object[] { value });
		Console.WriteLine(numberValue.GetValue(number));
	    }
	}
    }
}
