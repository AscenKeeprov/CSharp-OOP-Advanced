public class FileDownloadingFeedback : Feedback
{
    public override string AbortMessage => " Download cancelled.";
    public override string EndMessage => " File \"{0}\" has been overwritten.";
    public override string ResultMessage => " File \"{0}\" has been downloaded to your current working folder.";
}
