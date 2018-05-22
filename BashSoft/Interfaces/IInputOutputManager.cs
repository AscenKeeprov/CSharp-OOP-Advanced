using System.Collections.Generic;

public interface IInputOutputManager : IInputReader, IOutputWriter
{
    string BuildAbsolutePath(string path);
    string ExtractFileName(string path);
    void PrintDatabaseReport(Dictionary<string, Dictionary<string, int[]>> report);
}
