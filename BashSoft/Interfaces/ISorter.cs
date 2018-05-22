using System.Collections.Generic;

public interface ISorter
{
    Dictionary<string, Dictionary<string, int[]>> OrderReport(Dictionary<string, Dictionary<string, int[]>> report, string order);
}
