using System;

public class StudentAlreadyEnrolledInCourseException : Exception
{
    private string studentName;
    private string courseName;
    public override string Message => $" Student {studentName} is already enrolled in course {courseName}. Scores updated.";

    public StudentAlreadyEnrolledInCourseException(string studentName, string courseName)
    {
	this.studentName = studentName;
	this.courseName = courseName;
    }
}
