using System;
using System.Collections.Generic;

public interface IStudent : INamable, IComparable<IStudent>
{
    IDictionary<string, int[]> ScoresByCourse { get; }

    void EnrollInCourse(string courseName);
    void SetScoresForCourse(string courseName, int[] newScores);
    double CalculateGradeForCourse(string courseName);
}
