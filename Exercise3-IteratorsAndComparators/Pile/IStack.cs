using System.Collections.Generic;

public interface IStack<T> : IEnumerable<T>
{
    void Push(params T[] items);
    void Pop();
}
