using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>Carries out file system operations.</summary>
public class FSManager : IFileSystemManager
{
    private IServiceProvider Server;
    private IInputOutputManager IOManager => (IOManager)Server.GetService(typeof(IInputOutputManager));

    public string CurrentDirectory => Directory.GetCurrentDirectory();

    public FSManager(IServiceProvider serviceProvider)
    {
	Server = serviceProvider;
    }

    public void TraverseDirectory(int depth = 0)
    {
	string startDir = CurrentDirectory;
	int startDirLevel = startDir.Split('\\').Length;
	Queue<string> dirTree = new Queue<string>();
	dirTree.Enqueue(startDir);
	while (dirTree.Count != 0)
	{
	    string currentDir = dirTree.Dequeue();
	    int currentDirLevel = currentDir.Split('\\').Length;
	    if (currentDirLevel - startDirLevel == depth + 1) break;
	    try
	    {
		string[] currentDirSubdirs = Directory.GetDirectories(currentDir);
		string[] currentDirFiles = Directory.GetFiles(currentDir);
		if (currentDirSubdirs.Length == 0 && currentDirFiles.Length == 0)
		    IOManager.OutputLine(typeof(String),
			$"{new string(' ', currentDirLevel - startDirLevel)}{currentDir}");
		else
		{
		    IOManager.OutputLine(typeof(String),
			$"┌{new string('─', currentDirLevel - startDirLevel)}{currentDir}");
		    IOManager.DisplayDirectorySubdirectories(currentDirSubdirs, currentDirFiles, dirTree);
		    IOManager.DisplayDirectoryFiles(currentDirFiles);
		}
	    }
	    catch (Exception exception)
	    {
		if (exception is UnauthorizedAccessException)
		    IOManager.OutputLine(typeof(Exception), new InsufficientPrivilegesException().Message);
	    }
	}
    }

    public void CreateDirectory(string path)
    {
	string[] directoriesToCreate = path.Trim('\\', '/').Split('\\', '/');
	if (directoriesToCreate.Any(directoryName => String.IsNullOrWhiteSpace(directoryName)))
	    throw new InvalidValueException($"The provided {(directoriesToCreate.Length == 1 ? "directory name" : "path")}");
	string[] levelsUp = directoriesToCreate.Where(d => d == "..").ToArray();
	ChangeDirectory(String.Join("\\", levelsUp));
	directoriesToCreate = directoriesToCreate.Except(levelsUp).ToArray();
	string[] pathToCreate = IOManager.BuildAbsolutePath(String.Join("\\", directoriesToCreate))
	    .Split('\\', StringSplitOptions.RemoveEmptyEntries);
	string existingPath = String.Empty;
	try
	{
	    DirectoryCreationFeedback directoryCreationFeedback = new DirectoryCreationFeedback();
	    for (int level = 0; level < pathToCreate.Length; level++)
	    {
		string currentPath = existingPath + pathToCreate[level];
		if (!Directory.Exists(currentPath))
		{
		    Directory.CreateDirectory(currentPath);
		    IOManager.OutputLine(typeof(Feedback), String.Format(
			directoryCreationFeedback.ProgressMessage, pathToCreate[level], existingPath));
		}
		else if (path.Contains(':'))
		{
		    if (!pathToCreate[level].Contains(':'))
		    {
			IOManager.OutputLine(typeof(Feedback), String.Format(
			    directoryCreationFeedback.Message, pathToCreate[level],
			    !String.IsNullOrWhiteSpace(existingPath) ? existingPath : "ROOT"));
		    }
		}
		else if (level >= pathToCreate.Length - directoriesToCreate.Length)
		{
		    IOManager.OutputLine(typeof(Feedback), String.Format(
			    directoryCreationFeedback.Message, pathToCreate[level], existingPath));
		}
		existingPath = $"{currentPath}\\";
	    }
	}
	catch (Exception exception)
	{
	    if (exception is ArgumentException || exception is NotSupportedException)
		throw new InvalidNameException("Directory");
	    else if (exception is UnauthorizedAccessException)
		throw new InsufficientPrivilegesException();
	}
    }

    public void ChangeDirectory(string destinationPath)
    {
	destinationPath = IOManager.BuildAbsolutePath(destinationPath);
	try
	{
	    Directory.SetCurrentDirectory(destinationPath);
	}
	catch (Exception exception)
	{
	    if (exception is DirectoryNotFoundException)
		throw new InvalidPathException();
	    else if (exception is ArgumentOutOfRangeException)
		throw new InvalidCommandParameterException("Directory path");
	    else if (exception is UnauthorizedAccessException)
		throw new InsufficientPrivilegesException();
	}
    }

    public void CreateFile(string path, string[] fileContents)
    {
	string fileName = IOManager.ExtractFileName(path);
	if (String.IsNullOrWhiteSpace(fileName)) throw new FileNotSpecifiedException();
	path = IOManager.BuildAbsolutePath(path);
	string filePath = $"{path}\\{fileName}";
	try
	{
	    File.WriteAllLines(filePath, fileContents);
	}
	catch (Exception exception)
	{
	    if (exception is DirectoryNotFoundException)
		throw new InvalidPathException();
	    else if (exception is UnauthorizedAccessException)
		throw new InsufficientPrivilegesException();
	}
    }

    /// <summary>Tries to open a file and read it contents.
    /// <para>An error message appears if the file does not exist
    /// or the user does not have rights to work with it.</para></summary>
    public string[] ReadFile(string path)
    {
	string fileName = IOManager.ExtractFileName(path);
	if (String.IsNullOrWhiteSpace(fileName)) throw new FileNotSpecifiedException();
	path = IOManager.BuildAbsolutePath(path);
	string filePath = $"{path}\\{fileName}";
	try
	{
	    string[] fileContents = File.ReadAllLines(filePath);
	    return fileContents;
	}
	catch (Exception exception)
	{
	    if (exception is DirectoryNotFoundException || exception is FileNotFoundException)
		throw new InvalidPathException();
	    else if (exception is UnauthorizedAccessException)
		throw new InsufficientPrivilegesException();
	    return null;
	}
    }

    public void DownloadFile(string sourcePath)
    {
	string fileName = IOManager.ExtractFileName(sourcePath);
	if (String.IsNullOrWhiteSpace(fileName)) throw new FileNotSpecifiedException();
	sourcePath = IOManager.BuildAbsolutePath(sourcePath);
	string sourceFilePath = $"{sourcePath}\\{fileName}";
	string destinationFilePath = $"{CurrentDirectory}\\{fileName}";
	FileDownloadingFeedback fileDownloadFeedback = new FileDownloadingFeedback();
	try
	{
	    File.Copy(sourceFilePath, destinationFilePath);
	    IOManager.OutputLine(typeof(Feedback), String.Format(
		fileDownloadFeedback.ResultMessage, fileName));
	}
	catch (Exception exception)
	{
	    if (exception is DirectoryNotFoundException || exception is FileNotFoundException)
		throw new InvalidPathException();
	    else if (exception is UnauthorizedAccessException)
		throw new InsufficientPrivilegesException();
	    else if (exception is IOException)
	    {
		if (File.Exists(destinationFilePath))
		{
		    IOManager.Output(typeof(Exception), new FileAlreadyDownloadedException().Message);
		    ConsoleKeyInfo choice = Console.ReadKey();
		    while (choice.Key != ConsoleKey.Y && choice.Key != ConsoleKey.N)
		    {
			IOManager.Output(typeof(String), "\b \b");
			choice = Console.ReadKey();
		    }
		    IOManager.OutputLine();
		    if (choice.Key == ConsoleKey.Y)
		    {
			File.Copy(sourceFilePath, destinationFilePath, true);
			IOManager.OutputLine(typeof(Feedback), String.Format(
			    fileDownloadFeedback.EndMessage, fileName));
		    }
		    else if (choice.Key == ConsoleKey.N)
			IOManager.OutputLine(typeof(Feedback), fileDownloadFeedback.AbortMessage);
		}
		else throw exception;
	    }
	}
    }
}
