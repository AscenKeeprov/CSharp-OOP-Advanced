public interface IFileManager
{
    void CreateFile(string outputPath, string[] fileContents);
    string[] ReadFile(string path);
    void DownloadFile(string sourcePath);
}
