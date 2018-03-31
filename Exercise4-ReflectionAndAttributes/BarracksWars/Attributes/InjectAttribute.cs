using System;

[AttributeUsage(AttributeTargets.Field)]
public class InjectAttribute : Attribute
{
    public string Name { get; set; }

    public InjectAttribute(string name)
    {
	Name = name;
    }
}
