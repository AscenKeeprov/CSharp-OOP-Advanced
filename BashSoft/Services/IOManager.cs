using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

/// <summary>Processes input and presents output.</summary>
public class IOManager : IInputOutputManager
{
    private const string WelcomeFileLocation = "resources/welcome.txt";
    private const string FileNamePattern = @"(?!\s)[^\.\/\\:*?\""<>|]+\.[^\s\/\\:*?\""<>|]+";

    private IServiceProvider Server;
    private IFileSystemManager FSManager => (FSManager)Server.GetService(typeof(IFileSystemManager));
    private IUserInterface UserInterface => (UserInterface)Server.GetService(typeof(IUserInterface));
    private string separator = new String('░', 75);

    public IOManager(IServiceProvider serviceProvider)
    {
	Server = serviceProvider;
    }

    public void DisplayWelcome()
    {
	string currentPath = FSManager.CurrentDirectory;
	string appName = Assembly.GetExecutingAssembly().GetName().Name;
	int appDirectory = currentPath.IndexOf(appName) + appName.Length;
	string projectPath = currentPath.Substring(0, appDirectory);
	FSManager.ChangeDirectory(projectPath);
	string[] welcomeText = FSManager.ReadFile(WelcomeFileLocation);
	foreach (string line in welcomeText) OutputLine(typeof(String), line);
    }

    public int Input()
    {
	return Console.Read();
    }

    public string InputLine()
    {
	return Console.ReadLine();
    }

    public ConsoleKeyInfo InputKey()
    {
	return Console.ReadKey(true);
    }

    public void Output(Type outputType, string outputMessage = null)
    {
	if (!String.IsNullOrEmpty(outputMessage))
	{
	    UserInterface.FormatOutput(outputType);
	    Console.Write(outputMessage);
	    if (outputType != typeof(String))
		UserInterface.ResetOutputFormat();
	}
    }

    public void OutputLine()
    {
	Output(typeof(String), Environment.NewLine);
    }

    public void OutputLine(Type outputType, string message = null)
    {
	if (!String.IsNullOrEmpty(message))
	{
	    UserInterface.FormatOutput(outputType);
	    Console.WriteLine(message);
	    if (outputType != null) UserInterface.ResetOutputFormat();
	}
	else OutputLine();
    }

    public void DisplayDirectorySubdirectories(
	string[] subdirs, string[] currentDirFiles, Queue<string> dirTree)
    {
	for (int s = 0; s < subdirs.Length; s++)
	{
	    dirTree.Enqueue(subdirs[s]);
	    int subDirLevel = subdirs[s].LastIndexOf('\\');
	    string subDirName = subdirs[s].Substring(subDirLevel);
	    string indentation = $"├{new string('─', subDirLevel)}";
	    if (s == subdirs.Length - 1)
	    {
		if (currentDirFiles.Length > 0)
		    indentation = $"├{new string('─', subDirLevel)}";
		else indentation = $"└{new string('─', subDirLevel)}";
	    }
	    OutputLine(typeof(String), $"{indentation}{subDirName}");
	}
    }

    public void DisplayDirectoryFiles(string[] files)
    {
	for (int f = 0; f < files.Length; f++)
	{
	    int fileLevel = files[f].LastIndexOf('\\');
	    string fileName = files[f].Substring(fileLevel + 1);
	    string indentation = $"├{new string('─', fileLevel)}";
	    if (f == files.Length - 1) indentation = $"└{new string('─', fileLevel)}";
	    OutputLine(typeof(String), $"{indentation}{fileName}");
	}
    }

    public void DisplayFileContents(string[] fileContents)
    {
	OutputLine(typeof(String), separator);
	foreach (string line in fileContents)
	{
	    OutputLine(typeof(String), $"░ {line}");
	}
	OutputLine(typeof(String), separator);
    }

    public string BuildAbsolutePath(string path)
    {
	string currentDir = FSManager.CurrentDirectory;
	string absolutePath = path.Replace('/', '\\').Trim('\\');
	absolutePath = absolutePath.Replace("\\.\\", "\\");
	if (absolutePath.StartsWith(".\\") || absolutePath.EndsWith("\\."))
	    absolutePath = absolutePath.Trim('.').Trim('\\');
	if (!String.IsNullOrEmpty(ExtractFileName(absolutePath)))
	{
	    int pathEnd = absolutePath.LastIndexOf('\\');
	    if (pathEnd == -1) absolutePath = currentDir;
	    else absolutePath = absolutePath.Substring(0, pathEnd);
	}
	if (!absolutePath.Contains(':'))
	{
	    if (Regex.IsMatch(absolutePath, @"(?:\.{1,2}\\*)+"))
	    {
		string[] currentPath = currentDir.Split('\\');
		string targetDir = String.Empty;
		if (absolutePath.Contains('\\'))
		{
		    int targetDirIndex = Math.Min(absolutePath.LastIndexOf('.') + 2, absolutePath.Length);
		    targetDir = absolutePath.Substring(targetDirIndex);
		}
		if (!String.IsNullOrWhiteSpace(targetDir))
		    absolutePath = absolutePath.Replace(targetDir, String.Empty);
		int levelsUp = absolutePath.Split('\\', StringSplitOptions.RemoveEmptyEntries).Length;
		if (absolutePath == ".") levelsUp = 0;
		if (levelsUp < currentPath.Length)
		    absolutePath = $"{String.Join("\\", currentPath.Take(currentPath.Length - levelsUp))}\\{targetDir}";
		else absolutePath = $"{currentPath[0]}\\{targetDir}";
	    }
	    else
	    {
		if (!currentDir.EndsWith('\\'))
		    absolutePath = $"{currentDir}\\{absolutePath}";
		else absolutePath = currentDir + absolutePath;
	    }
	}
	else if (absolutePath.EndsWith(':')) absolutePath += "\\";
	return absolutePath;
    }

    public string ExtractFileName(string path)
    {
	string fileName = String.Empty;
	if (path.Contains('.'))
	{
	    if (path.Contains('\\') || path.Contains('/'))
	    {
		string tail = Regex.Split(path, @".*\\|.*\/").Last();
		if (Regex.IsMatch(tail, FileNamePattern)) fileName = tail;
	    }
	    else
	    {
		if (Regex.IsMatch(path, FileNamePattern)) fileName = path;
	    }
	}
	return fileName;
    }

    public void PrintDatabaseReport(Dictionary<string, Dictionary<string, int[]>> report)
    {
	OutputLine(typeof(Feedback), new DatabaseReportingFeedback().ResultMessage);
	OutputLine(typeof(String), separator);
	foreach (var record in report)
	{
	    string course = record.Key;
	    OutputLine(typeof(String), $"░·{course}:");
	    var studentData = record.Value;
	    foreach (var student in studentData)
	    {
		string studentName = student.Key;
		int[] scores = student.Value;
		double averageScore = scores.Average();
		double grade = (averageScore / 100) * 4 + 2;
		OutputLine(typeof(String), $"░  {studentName} ─ {String.Join(",", scores)}" +
		    $" ═> Average Score: {averageScore:F0}, Grade: {grade:F2}");
	    }
	}
	OutputLine(typeof(String), separator);
    }
}
