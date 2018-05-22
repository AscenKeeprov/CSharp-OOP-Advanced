using System;
using System.Collections.Generic;
using System.Linq;

public class User
{
    public string Name { get; private set; }
    private HashSet<Category> subscriptions;

    public User(string name)
    {
	Name = name;
	subscriptions = new HashSet<Category>();
    }

    public void Subscribe(Category category)
    {
	if (subscriptions.Any(c => c.Name == category.Name))
	    throw new InvalidOperationException($"{Name} is already subscribed to category {category.Name}!");
    }

    public void Subscribe(string categoryName)
    {
	if (!subscriptions.Any(c => c.Name == categoryName))
	    throw new ArgumentException($"{Name} is not subscribed to category {categoryName}!");
    }
}
