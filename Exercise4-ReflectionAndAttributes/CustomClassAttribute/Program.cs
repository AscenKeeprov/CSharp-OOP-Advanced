using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CustomClassAttribute
{
    [Custom("Pesho", 3, "Used for C# OOP Advanced Course - Enumerations and Attributes.", "Pesho", "Svetlio")]
    class Program
    {
	static void Main()
	{
	    TextInfo textInfo = new CultureInfo("", true).TextInfo;
	    Type customAttributeType = typeof(CustomAttribute);
	    var customAttributeFields = customAttributeType.GetFields(
		BindingFlags.Instance | BindingFlags.Static |
		BindingFlags.Public | BindingFlags.NonPublic);
	    Type classWithCustomAttribute = Assembly.GetExecutingAssembly()
		.GetTypes().Where(t => t.CustomAttributes.Any(a => a
		.AttributeType == typeof(CustomAttribute))).First();
	    var customAttributeInstance = classWithCustomAttribute
		.GetCustomAttribute(typeof(CustomAttribute));
	    string command;
	    while (!(command = Console.ReadLine().ToUpper()).Equals("END"))
	    {
		FieldInfo customAttributeField = customAttributeFields
			    .SingleOrDefault(f => f.Name.ToUpper() == command);
		var fieldName = customAttributeField.Name;
		object fieldValue = customAttributeField.GetValue(customAttributeInstance);
		string output = String.Empty;
		switch (fieldName.ToUpper())
		{
		    case "AUTHOR":
		    case "REVISION":
			output = $"{textInfo.ToTitleCase(fieldName)}: {fieldValue}";
			break;
		    case "DESCRIPTION":
			output = $"Class {fieldName.ToLower()}: {fieldValue}";
			break;
		    case "REVIEWERS":
			output = $"{textInfo.ToTitleCase(fieldName)}: {String.Join(", ", (string[])fieldValue)}";
			break;
		}
		Console.WriteLine(output);
	    }
	}
    }
}
