using System;
using System.Collections.Generic;
using System.Linq;

public class Student : IStudent
{
    private string name;

    public IDictionary<string, int[]> ScoresByCourse { get; }

    public string Name
    {
	get { return name; }
	private set
	{
	    if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
		throw new InvalidValueException($"{GetType().Name} {nameof(name)}");
	    name = value;
	}
    }

    internal Student()
    {
	ScoresByCourse = new Dictionary<string, int[]>();
    }

    internal Student(string name) : this()
    {
	Name = name;
    }

    internal Student(string name, string courseName, int[] scores) : this(name)
    {
	EnrollInCourse(courseName);
	SetScoresForCourse(courseName, scores);
    }

    public int CompareTo(IStudent otherStudent)
    {
	return Name.CompareTo(otherStudent.Name);
    }

    public void EnrollInCourse(string courseName)
    {
	if (ScoresByCourse.ContainsKey(courseName))
	    throw new StudentAlreadyEnrolledInCourseException(Name, courseName);
	else ScoresByCourse.Add(courseName, null);
    }

    public void SetScoresForCourse(string courseName, int[] newScores)
    {
	if (!ScoresByCourse.ContainsKey(courseName))
	    throw new StudentNotEnrolledInCourseException(Name, courseName);
	if (newScores.Length > Course.NumberOfTasksPerExam)
	{
	    if (ScoresByCourse[courseName] == null)
		ScoresByCourse.Remove(courseName);
	    throw new InvalidNumberOfScoresException(Name, courseName);
	}
	int[] oldScores = ScoresByCourse.First(c => c.Key == courseName).Value;
	if (oldScores == null) ScoresByCourse[courseName] = newScores.ToArray();
	else
	{
	    for (int s = 0; s < newScores.Length; s++)
	    {
		if (newScores[s] > oldScores[s]) oldScores[s] = newScores[s];
	    }
	}
    }

    public double CalculateGradeForCourse(string courseName)
    {
	int[] scores = ScoresByCourse.First(c => c.Key == courseName).Value;
	double grade = (scores.Average() / 100) * 4 + 2;
	return grade;
    }

    public override string ToString()
    {
	return $"{Name} ({ScoresByCourse.Keys.Count} Courses)";
    }
}
