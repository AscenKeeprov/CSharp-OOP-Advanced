using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>Enables data persistence and retrieval.</summary>
public class Repository : IRepository
{
    private const string DBRecordPattern = @"^(?<Course>[A-Z]\#?\+{0,2}[a-zA-Z]*_[A-Z][a-z]{2}_201[4-8])\s+(?<Student>(?:[A-Z][a-z]+){2,}\d{2}_\d{2,4})\s+(?<Scores>(?:100\s?|[1-9][0-9]\s?|[0-9]\s?){1,})$";

    private IServiceProvider Server;
    private IFilter Sifter => (Filter)Server.GetService(typeof(IFilter));
    private ISorter Sorter => (Sorter)Server.GetService(typeof(ISorter));
    private IFileSystemManager FSManager => (FSManager)Server.GetService(typeof(IFileSystemManager));
    private IInputOutputManager IOManager => (IOManager)Server.GetService(typeof(IInputOutputManager));
    private ICollection<ICourse> Courses;
    private ICollection<IStudent> Students;
    private bool IsDatabaseInitialized => Courses.Count > 0;

    public Repository(IServiceProvider serviceProvider)
    {
	Server = serviceProvider;
	Courses = new List<ICourse>();
	Students = new List<IStudent>();
    }

    public void LoadData(string path)
    {
	var dbInitFeedback = new DatabaseInitializationFeedback();
	if (!IsDatabaseInitialized)
	{
	    string[] databaseSource = FSManager.ReadFile(path);
	    IOManager.OutputLine(typeof(Feedback), dbInitFeedback.BeginMessage);
	    if (databaseSource.Length == 0) throw new DatabaseSourceEmptyException();
	    IOManager.OutputLine(typeof(Feedback), dbInitFeedback.ProgressMessage);
	    ICollection<string> exceptionMessages = new HashSet<string>();
	    foreach (string record in databaseSource.Where(r => IsRecordValid(r)))
	    {
		Match validRecord = Regex.Match(record, DBRecordPattern);
		ICourse course = new Course(validRecord.Groups["Course"].Value);
		IStudent student = new Student(validRecord.Groups["Student"].Value);
		int[] scores = validRecord.Groups["Scores"].Value.Split().Select(int.Parse).ToArray();
		if (!Courses.Any(c => c.Name == course.Name)) Courses.Add(course);
		else course = Courses.SingleOrDefault(c => c.Name == course.Name);
		if (!Students.Any(s => s.Name == student.Name)) Students.Add(student);
		else student = Students.SingleOrDefault(s => s.Name == student.Name);
		try { course.EnrollStudent(student.Name); }
		catch (Exception exception) { exceptionMessages.Add(exception.Message); }
		try { course.SetScoresForStudent(student.Name, scores); }
		catch (Exception exception) { exceptionMessages.Add(exception.Message); }
		try { student.EnrollInCourse(course.Name); }
		catch (Exception exception) { exceptionMessages.Add(exception.Message); }
		try { student.SetScoresForCourse(course.Name, scores); }
		catch (Exception exception) { exceptionMessages.Add(exception.Message); }
	    }
	    foreach (string exceptionMessage in exceptionMessages)
		IOManager.OutputLine(typeof(Exception), exceptionMessage);
	    IOManager.OutputLine(typeof(Feedback), dbInitFeedback.EndMessage);
	    IOManager.OutputLine(typeof(Feedback), String.Format(
		dbInitFeedback.ResultMessage, Students.Count));
	}
	else
	{
	    IOManager.Output(typeof(Exception), new DatabaseAlreadyInitializedException().Message);
	    ConsoleKeyInfo choice = Console.ReadKey();
	    while (choice.Key != ConsoleKey.Y && choice.Key != ConsoleKey.N)
	    {
		IOManager.Output(typeof(String), "\b \b");
		choice = Console.ReadKey();
	    }
	    IOManager.OutputLine();
	    if (choice.Key == ConsoleKey.Y) DeleteData();
	    else if (choice.Key == ConsoleKey.N)
		IOManager.OutputLine(typeof(Feedback), dbInitFeedback.AbortMessage);
	}
    }

    private bool IsRecordValid(string record)
    {
	return !String.IsNullOrEmpty(record) && Regex.IsMatch(record, DBRecordPattern);
    }

    public void ReadData(string courseName, string studentName, string filter, string order)
    {
	if (IsDatabaseInitialized)
	{
	    if (IsQueryValid(courseName, studentName, filter, order))
	    {
		ICollection<ICourse> coursesToShow = Sifter.FilterCourses(Courses, courseName);
		if (HasZeroRecords(coursesToShow)) return;
		ICollection<IStudent> studentsToShow = Sifter.FilterScores(coursesToShow, filter);
		if (HasZeroRecords(studentsToShow)) return;
		studentsToShow = Sifter.FilterStudents(studentsToShow, studentName);
		if (HasZeroRecords(studentsToShow)) return;
		var report = PrepareDatabaseReport(studentsToShow);
		report = Sorter.OrderReport(report, order);
		report = Sifter.FilterStudents(report, studentName);
		if (HasZeroRecords(report)) return;
		IOManager.PrintDatabaseReport(report);
	    }
	}
	else throw new DatabaseNotInitializedException();
    }

    private bool IsQueryValid(string courseName, string studentName, string filter, string order)
    {
	if (!Courses.Any(c => c.Name == courseName) && !courseName.ToUpper().Equals("ANY"))
	    throw new InvalidCommandParameterException("Course criterion");
	if (!Students.Any(s => s.Name == studentName) && !studentName.ToUpper().Equals("ALL") &&
	    !int.TryParse(studentName, out int number))
	    throw new InvalidCommandParameterException("Student criterion");
	if (!Enum.IsDefined(typeof(EFilter), filter))
	    throw new InvalidCommandParameterException("Report filter");
	if (!Enum.IsDefined(typeof(EOrder), order))
	    throw new InvalidCommandParameterException("Report order");
	return true;
    }

    private bool HasZeroRecords<T>(ICollection<T> collection)
    {
	if (collection.Count > 0) return false;
	IOManager.OutputLine(typeof(Feedback), String.Format(
	    new DatabaseReportingFeedback().ProgressMessage, "No"));
	return true;
    }

    private bool HasZeroRecords<T, Array>(Dictionary<T, Dictionary<T, Array>> collection)
    {
	if (collection.Values.All(v => v.Count > 0)) return false;
	IOManager.OutputLine(typeof(Feedback), String.Format(
	    new DatabaseReportingFeedback().ProgressMessage, "No"));
	return true;
    }

    private Dictionary<string, Dictionary<string, int[]>> PrepareDatabaseReport(
	ICollection<IStudent> studentsToShow)
    {
	IOManager.OutputLine(typeof(Feedback), new DatabaseReportingFeedback().BeginMessage);
	var report = new Dictionary<string, Dictionary<string, int[]>>();
	foreach (IStudent student in studentsToShow)
	{
	    foreach (var course in student.ScoresByCourse)
	    {
		int[] studentScores = course.Value;
		if (!report.ContainsKey(course.Key))
		    report.Add(course.Key, new Dictionary<string, int[]>());
		if (!report[course.Key].ContainsKey(student.Name))
		    report[course.Key].Add(student.Name, new int[Course.NumberOfTasksPerExam]);
		report[course.Key][student.Name] = studentScores;
	    }
	}
	return report;
    }

    public void DeleteData()
    {
	var databaseDeletionFeedback = new DatabaseDeletionFeedback();
	if (!IsDatabaseInitialized)
	{
	    IOManager.OutputLine(typeof(Feedback), databaseDeletionFeedback.EndMessage);
	    IOManager.OutputLine(typeof(Feedback), databaseDeletionFeedback.Message);
	    return;
	}
	IOManager.Output(typeof(Feedback), databaseDeletionFeedback.ProgressMessage);
	ConsoleKeyInfo choice = Console.ReadKey();
	while (choice.Key != ConsoleKey.Y && choice.Key != ConsoleKey.N)
	{
	    IOManager.Output(typeof(String), "\b \b");
	    choice = Console.ReadKey();
	}
	IOManager.OutputLine();
	if (choice.Key == ConsoleKey.Y)
	{
	    IOManager.OutputLine(typeof(Feedback), databaseDeletionFeedback.BeginMessage);
	    Courses.Clear();
	    Students.Clear();
	    IOManager.OutputLine(typeof(Feedback), databaseDeletionFeedback.ResultMessage);
	    IOManager.OutputLine(typeof(Feedback), databaseDeletionFeedback.Message);
	}
	else if (choice.Key == ConsoleKey.N)
	    IOManager.OutputLine(typeof(Feedback), databaseDeletionFeedback.AbortMessage);
    }
}
