public interface ILogFile
{
    string Path { get; }
    int Size { get; }

    void Write(string output);
}
