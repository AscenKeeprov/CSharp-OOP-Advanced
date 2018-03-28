using System.Collections.Generic;

public interface IListIterator<T> : IEnumerable<T>
{
    bool Move();
    bool HasNext();
    T Print();
    string PrintAll();
}
