using System;
using System.Collections.Generic;

public interface IOutputWriter
{
    void Output(Type outputType, string outputMessage);
    void OutputLine();
    void OutputLine(Type outputType, string outputMessage);
    void DisplayWelcome();
    void DisplayDirectorySubdirectories(string[] subdirs, string[] currentDirFiles, Queue<string> dirTree);
    void DisplayDirectoryFiles(string[] files);
    void DisplayFileContents(string[] fileContents);
}
