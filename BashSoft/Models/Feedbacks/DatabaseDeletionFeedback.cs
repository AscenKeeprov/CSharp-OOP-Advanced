public class DatabaseDeletionFeedback : Feedback
{
    public override string Message => " Please execute the LOADDB command to populate the database.";
    public override string BeginMessage => " Erasing database records...";
    public override string ProgressMessage => " Are you sure you want to erase all database records? (Y/N) ";
    public override string AbortMessage => " Database deletion cancelled.";
    public override string EndMessage => " The database is empty! No records to remove.";
    public override string ResultMessage => " Done. All database records were removed.";
}
