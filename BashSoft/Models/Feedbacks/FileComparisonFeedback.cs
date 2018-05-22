public class FileComparisonFeedback : Feedback
{
    public override string Message => "■ Mismatch at line {0} ─ Expected: \"{1}\" <=> Actual: \"{2}\"";
    public override string BeginMessage => " Comparing files...";
    public override string ProgressMessage => " Comparison results:";
    public override string AbortMessage => " Comparison cancelled.";
    public override string EndMessage => " Files are identical. There are no mismatches.";
    public override string ResultMessage => " Results saved in \"{0}\".";
}
