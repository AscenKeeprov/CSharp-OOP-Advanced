using System;
using System.Collections.Generic;
using System.Linq;

public class Course : ICourse
{
    internal const int NumberOfTasksPerExam = 5;
    internal const int MaxScorePerExamTask = 100;

    private string name;

    public IDictionary<string, int[]> ScoresByStudent { get; }

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

    internal Course()
    {
	ScoresByStudent = new Dictionary<string, int[]>();
    }

    internal Course(string name) : this()
    {
	Name = name;
    }

    internal Course(string name, string studentName, int[] scores) : this(name)
    {
	EnrollStudent(studentName);
	SetScoresForStudent(studentName, scores);
    }

    public int CompareTo(ICourse otherCourse)
    {
	return Name.CompareTo(otherCourse.Name);
    }

    public void EnrollStudent(string studentName)
    {
	if (ScoresByStudent.Any(s => s.Key == studentName))
	    throw new StudentAlreadyEnrolledInCourseException(studentName, Name);
	else ScoresByStudent.Add(studentName, null);
    }

    public void SetScoresForStudent(string studentName, int[] newScores)
    {
	if (!ScoresByStudent.ContainsKey(studentName))
	    throw new StudentNotEnrolledInCourseException(studentName, Name);
	if (newScores.Length > NumberOfTasksPerExam)
	{
	    if (ScoresByStudent[studentName] == null)
		ScoresByStudent.Remove(studentName);
	    throw new InvalidNumberOfScoresException(studentName, Name);
	}
	int[] oldScores = ScoresByStudent.First(s => s.Key == studentName).Value;
	if (oldScores == null) ScoresByStudent[studentName] = newScores.ToArray();
	else
	{
	    for (int s = 0; s < newScores.Length; s++)
	    {
		if (newScores[s] > oldScores[s]) oldScores[s] = newScores[s];
	    }
	}
    }

    public double CalculateGradeForStudent(string studentName)
    {
	int[] scores = ScoresByStudent.First(s => s.Key == studentName).Value;
	double grade = (scores.Average() / 100) * 4 + 2;
	return grade;
    }

    public override string ToString()
    {
	return $"{Name} ({ScoresByStudent.Keys.Count} Attendees)";
    }
}
