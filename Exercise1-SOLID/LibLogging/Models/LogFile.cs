using System;
using System.IO;
using System.Reflection;

public class LogFile : ILogFile
{
    private const string LOGFILE_DIRECTORY = "Logs\\";
    private const string LOGFILE_NAME = "log.txt";

    public string Location => GetLoggerDirectory() + LOGFILE_DIRECTORY;
    public string Path => Location + LOGFILE_NAME;
    public int Size => GetSize();

    public LogFile()
    {
	EnsureLogsDirectory();
    }

    private void EnsureLogsDirectory()
    {
	if (!Directory.Exists(Location))
	    Directory.CreateDirectory(Location);
    }

    private string GetLoggerDirectory()
    {
	string assemblyLocation = Assembly.GetExecutingAssembly().Location;
	return assemblyLocation.Substring(0, assemblyLocation.LastIndexOf("\\bin") + 1);
    }

    private int GetSize()
    {
	if (!File.Exists(Path)) return 0;
	int logFileSize = 0;
	string logFileContents = File.ReadAllText(Path);
	foreach (char symbol in logFileContents)
	{
	    if (Char.IsLetter(symbol)) logFileSize += symbol;
	}
	return logFileSize;
    }

    public void Write(string output)
    {
	File.AppendAllText(Path, output);
    }
}
