public interface ICache<T>
{
    void Add(T item);
    string Next();
    string Previous();
}
