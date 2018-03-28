public class Box<T> : IBox
{
    public T Contents { get; }

    public Box(T contents)
    {
	Contents = contents;
    }

    public override string ToString()
    {
	return $"{typeof(T)}: {Contents}";
    }
}
