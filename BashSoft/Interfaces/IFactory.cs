public interface IFactory<T>
{
    T Create(params string[] productParameters);
}