public class Item<T> : IItem
{
    public T Value { get; }

    public Item(T value)
    {
	Value = value;
    }

    public override string ToString()
    {
	return $"{typeof(T)}: {Value}";
    }
}
