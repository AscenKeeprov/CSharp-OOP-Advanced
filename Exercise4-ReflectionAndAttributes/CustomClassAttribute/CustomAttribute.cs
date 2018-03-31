using System;

[AttributeUsage(AttributeTargets.Class)]
public class CustomAttribute : Attribute
{
    private string author;
    private int revision;
    private string description;
    private string[] reviewers;

    public CustomAttribute(string author, int revision, string description, params string[] reviewers)
    {
	this.author = author;
	this.revision = revision;
	this.description = description;
	this.reviewers = reviewers;
    }
}
