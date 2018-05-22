using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>Applies filtering to database reports.</summary>
public class Filter : IFilter
{
    public ICollection<ICourse> FilterCourses(ICollection<ICourse> courses, string courseName)
    {
	ICollection<ICourse> coursesToShow = new List<ICourse>();
	if (!courseName.ToUpper().Equals("ANY"))
	    coursesToShow = courses.Where(c => c.Name == courseName).ToList();
	else coursesToShow = courses.ToList();
	return coursesToShow;
    }

    public ICollection<IStudent> FilterScores(ICollection<ICourse> coursesToShow, string filter)
    {
	ICollection<IStudent> studentsToShow = new List<IStudent>();
	if (!filter.Equals(EFilter.OFF.ToString()))
	{
	    double minGrade = 2.00;
	    double maxGrade = 6.01;
	    if (filter.Equals(EFilter.EXCELLENT.ToString())) minGrade = 5.00;
	    else if (filter.Equals(EFilter.AVERAGE.ToString()))
	    {
		minGrade = 3.50;
		maxGrade = 5.00;
	    }
	    else if (filter.Equals(EFilter.POOR.ToString())) maxGrade = 3.50;
	    foreach (ICourse course in coursesToShow)
	    {
		foreach (var studentScores in course.ScoresByStudent)
		{
		    string studentName = studentScores.Key;
		    double studentGrade = course.CalculateGradeForStudent(studentName);
		    if (studentGrade >= minGrade && studentGrade < maxGrade)
		    {
			IStudent student = new Student(studentName);
			if (!studentsToShow.Any(s => s.Name == studentName))
			    studentsToShow.Add(student);
			else student = studentsToShow.First(s => s.Name == studentName);
			student.ScoresByCourse.Add(course.Name, studentScores.Value);
		    }
		}
	    }
	}
	else
	{
	    foreach (ICourse course in coursesToShow)
	    {
		foreach (var studentScores in course.ScoresByStudent)
		{
		    string studentName = studentScores.Key;
		    IStudent student = new Student(studentName);
		    if (!studentsToShow.Any(s => s.Name == studentName))
			studentsToShow.Add(student);
		    else student = studentsToShow.First(s => s.Name == studentName);
		    student.ScoresByCourse.Add(course.Name, studentScores.Value);
		}
	    }
	}
	return studentsToShow;
    }

    public ICollection<IStudent> FilterStudents(ICollection<IStudent> studentsToShow, string studentName)
    {
	if (!studentName.ToUpper().Equals("ALL") && !int.TryParse(studentName, out int number))
	    studentsToShow = studentsToShow.Where(s => s.Name == studentName).ToList();
	return studentsToShow;
    }

    public Dictionary<string, Dictionary<string, int[]>> FilterStudents(
	Dictionary<string, Dictionary<string, int[]>> report, string studentName)
    {
	if (!studentName.ToUpper().Equals("ALL"))
	{
	    var filteredReport = new Dictionary<string, Dictionary<string, int[]>>();
	    if (int.TryParse(studentName, out int studentsToTake))
	    {
		foreach (var course in report)
		{
		    var scoresByStudent = course.Value;
		    int numberOfStudents = Math.Min(scoresByStudent.Count, studentsToTake);
		    scoresByStudent = scoresByStudent.Take(numberOfStudents)
			.ToDictionary(student => student.Key, scores => scores.Value);
		    filteredReport.Add(course.Key, scoresByStudent);
		}
	    }
	    else
	    {
		foreach (var course in report)
		{
		    var scoresByStudent = course.Value;
		    scoresByStudent = scoresByStudent.Where(student => student.Key == studentName)
			.ToDictionary(student => student.Key, scores => scores.Value);
		    if (scoresByStudent.Keys.Count > 0)
			filteredReport.Add(course.Key, scoresByStudent);
		}
	    }
	    return filteredReport;
	}
	return report;
    }
}
