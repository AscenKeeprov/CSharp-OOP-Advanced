using System;

public class InvalidNumberOfScoresException : InvalidValueException
{
    public InvalidNumberOfScoresException(string studentName, string courseName) : base(studentName, courseName)
    {
	ValueName = $"The number of scores for student {studentName}";
	ReasonForException = $" It exceeds the number of exam tasks for course {courseName}. Record skipped.";
    }
}
