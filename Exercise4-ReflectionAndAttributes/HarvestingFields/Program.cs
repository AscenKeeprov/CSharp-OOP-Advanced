using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarvestingFields
{
    public class Program
    {
	public static void Main()
	{
	    Type land = Type.GetType("RichSoilLand");
	    IEnumerable<FieldInfo> landFields = land.GetRuntimeFields();
	    string command;
	    while (!(command = Console.ReadLine()).Equals("HARVEST"))
	    {
		switch (command.ToUpper())
		{
		    case "PRIVATE":
			foreach (var field in landFields.Where(f => f.IsPrivate))
			    Console.WriteLine($"private {field.FieldType.Name} {field.Name}");
			break;
		    case "PROTECTED":
			foreach (var field in landFields.Where(f => f.IsFamily))
			    Console.WriteLine($"protected {field.FieldType.Name} {field.Name}");
			break;
		    case "PUBLIC":
			foreach (var field in landFields.Where(f => f.IsPublic))
			    Console.WriteLine($"public {field.FieldType.Name} {field.Name}");
			break;
		    case "ALL":
			foreach (var field in landFields)
			{
			    string accessModifier = String.Empty;
			    if (field.IsPrivate) accessModifier = "private";
			    if (field.IsFamily) accessModifier = "protected";
			    if (field.IsPublic) accessModifier = "public";
			    Console.WriteLine($"{accessModifier} {field.FieldType.Name} {field.Name}");
			}
			break;
		}
	    }
	}
    }
}
