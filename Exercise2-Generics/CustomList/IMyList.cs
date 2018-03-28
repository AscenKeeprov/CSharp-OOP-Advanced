public interface IMyList<T>
{
    void Add(T item);
    T Remove(int index);
    bool Contains(T item);
    void Swap(int index1, int index2);
    int CountGreaterThan(T item);
    T Max();
    T Min();
    void Sort();
}
