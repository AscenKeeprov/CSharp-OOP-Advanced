using System;
using System.Collections.Generic;
using System.Linq;

public class Category
{
    public string Name { get; private set; }
    private HashSet<User> subscribers;
    private HashSet<Category> subcategories;

    public Category(string name)
    {
	Name = name;
	subscribers = new HashSet<User>();
	subcategories = new HashSet<Category>();
    }

    public void AddSubcategory(Category category)
    {
	if (subcategories.Any(c => c.Name == category.Name))
	    throw new InvalidOperationException($"Category {category.Name} already exists!");
	subcategories.Add(category);
    }

    public void AddSubscriber(User user)
    {
	if (subscribers.Any(u => u.Name == user.Name))
	    throw new InvalidOperationException($"{user.Name} is already subscribed to category {Name}!");
	subscribers.Add(user);
    }
}
