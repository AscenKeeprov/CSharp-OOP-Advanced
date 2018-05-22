using System;

/// <summary>SoftUni Judge® simulator.</summary>
public class Tester : ITester
{
    private IServiceProvider Server;
    private IInputOutputManager IOManager => (IOManager)Server.GetService(typeof(IInputOutputManager));
    private IFileSystemManager FSManager => (FSManager)Server.GetService(typeof(IFileSystemManager));

    public Tester(IServiceProvider serviceProvider)
    {
	Server = serviceProvider;
    }

    public void CompareFiles(string userOutputPath, string expectedOutputPath)
    {
	FileReadingFeedback fileReadingFeedback = new FileReadingFeedback();
	IOManager.OutputLine(typeof(Feedback), fileReadingFeedback.BeginMessage);
	FileComparisonFeedback fileComparisonFeedback = new FileComparisonFeedback();
	string[] actualOutput = FSManager.ReadFile(userOutputPath);
	string[] expectedOutput = FSManager.ReadFile(expectedOutputPath);
	IOManager.OutputLine(typeof(Feedback), fileReadingFeedback.ProgressMessage);
	string[] comparisonResults = Compare(actualOutput, expectedOutput, out bool hasMismatch, out bool isComparisonCancelled);
	if (hasMismatch && !isComparisonCancelled)
	{
	    string mismatchesPath = IOManager.BuildAbsolutePath(expectedOutputPath);
	    string mismatchesFilePath = $"{mismatchesPath}\\mismatches.txt";
	    FSManager.CreateFile(mismatchesFilePath, comparisonResults);
	    string[] mismatches = FSManager.ReadFile(mismatchesFilePath);
	    IOManager.OutputLine(typeof(Feedback), fileComparisonFeedback.ProgressMessage);
	    IOManager.DisplayFileContents(mismatches);
	    IOManager.OutputLine(typeof(Feedback), String.Format(
		fileComparisonFeedback.ResultMessage, mismatchesFilePath));
	}
	else if (comparisonResults != null)
	    IOManager.OutputLine(typeof(Feedback), fileComparisonFeedback.EndMessage);
    }

    private string[] Compare(string[] actualOutput, string[] expectedOutput, out bool hasMismatch, out bool isComparisonCancelled)
    {
	FileComparisonFeedback fileComparisonFeedback = new FileComparisonFeedback();
	IOManager.OutputLine(typeof(Feedback), fileComparisonFeedback.BeginMessage);
	hasMismatch = false;
	isComparisonCancelled = false;
	if (actualOutput.Length != expectedOutput.Length)
	{
	    hasMismatch = true;
	    IOManager.Output(typeof(Exception), new FileMismatchInevitableException().Message);
	    ConsoleKeyInfo choice = Console.ReadKey();
	    while (choice.Key != ConsoleKey.Y && choice.Key != ConsoleKey.N)
	    {
		IOManager.Output(typeof(String), "\b \b");
		choice = Console.ReadKey();
	    }
	    IOManager.OutputLine();
	    if (choice.Key == ConsoleKey.N)
	    {
		isComparisonCancelled = true;
		IOManager.OutputLine(typeof(Feedback), fileComparisonFeedback.AbortMessage);
	    }
	}
	if (!isComparisonCancelled)
	{
	    string output = String.Empty;
	    int minOutputLength = Math.Min(actualOutput.Length, expectedOutput.Length);
	    string[] mismatches = new string[minOutputLength];
	    for (int line = 0; line < minOutputLength; line++)
	    {
		string actualLine = actualOutput[line];
		string expectedLine = expectedOutput[line];
		if (!actualLine.Equals(expectedLine))
		{
		    hasMismatch = true;
		    string mismatchLine = String.Format(
			fileComparisonFeedback.Message, line + 1, expectedLine, actualLine);
		    output = mismatchLine;
		}
		else output = actualLine;
		mismatches[line] = output;
	    }
	    return mismatches;
	}
	else return null;
    }
}
