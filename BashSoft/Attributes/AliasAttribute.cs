using System;

[AttributeUsage(AttributeTargets.Class)]
public class AliasAttribute : Attribute
{
    private string alias;

    public AliasAttribute(string alias)
    {
	this.alias = alias;
    }
}
