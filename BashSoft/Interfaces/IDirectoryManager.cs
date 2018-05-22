public interface IDirectoryManager
{
    string CurrentDirectory { get; }

    void TraverseDirectory(int traversalDepth);
    void CreateDirectory(string path);
    void ChangeDirectory(string destinationPath);
}
