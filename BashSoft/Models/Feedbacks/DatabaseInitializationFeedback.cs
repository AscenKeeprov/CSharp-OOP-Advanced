public class DatabaseInitializationFeedback : Feedback
{
    public override string BeginMessage => " Reading data...";
    public override string ProgressMessage => " Populating database structure...";
    public override string AbortMessage => " Database initialization cancelled.";
    public override string EndMessage => " Database initialization complete!";
    public override string ResultMessage => " A total of {0} student records were loaded.";
}
