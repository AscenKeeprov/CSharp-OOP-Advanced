public class DatabaseReportingFeedback : Feedback
{
    public override string BeginMessage => " Generating report...";
    public override string ProgressMessage => " {0} database records match your query.";
    public override string ResultMessage => " The following results match your query:";
}
