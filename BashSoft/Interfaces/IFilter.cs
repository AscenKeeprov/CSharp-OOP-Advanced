using System.Collections.Generic;

public interface IFilter
{
    ICollection<ICourse> FilterCourses(ICollection<ICourse> Courses, string courseName);
    ICollection<IStudent> FilterScores(ICollection<ICourse> coursesToShow, string filter);
    ICollection<IStudent> FilterStudents(ICollection<IStudent> studentsToShow, string studentName);
    Dictionary<string, Dictionary<string, int[]>> FilterStudents(Dictionary<string, Dictionary<string, int[]>> report, string studentName);
}
