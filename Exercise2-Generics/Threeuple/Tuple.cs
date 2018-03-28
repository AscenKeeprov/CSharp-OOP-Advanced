public class Tuple<T1, T2, T3>
{
    T1 Item1 { get; }
    T2 Item2 { get; }
    T3 Item3 { get; }

    public Tuple(T1 item1, T2 item2, T3 item3)
    {
	Item1 = item1;
	Item2 = item2;
	Item3 = item3;
    }

    public override string ToString()
    {
	return $"{Item1} -> {Item2} -> {Item3}";
    }
}
