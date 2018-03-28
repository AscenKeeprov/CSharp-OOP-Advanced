public class Tuple<T1,T2>
{
    T1 Item1 { get; }
    T2 Item2 { get; }

    public Tuple(T1 item1, T2 item2)
    {
	Item1 = item1;
	Item2 = item2;
    }

    public override string ToString()
    {
	return $"{Item1} -> {Item2}";
    }
}
