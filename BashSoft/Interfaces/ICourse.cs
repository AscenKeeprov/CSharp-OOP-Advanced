using System;
using System.Collections.Generic;

public interface ICourse : INamable, IComparable<ICourse>
{
    IDictionary<string, int[]> ScoresByStudent { get; }

    void EnrollStudent(string studentName);
    void SetScoresForStudent(string studentName, int[] newScores);
    double CalculateGradeForStudent(string studentName);
}
