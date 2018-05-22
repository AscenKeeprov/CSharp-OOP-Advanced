using System;

public class StudentNotEnrolledInCourseException : Exception
{
    private string studentName;
    private string courseName;
    public override string Message => $" Student {studentName} must be enrolled in course {courseName} before registering any marks!";

    public StudentNotEnrolledInCourseException(string studentName, string courseName)
    {
	this.studentName = studentName;
	this.courseName = courseName;
    }
}
