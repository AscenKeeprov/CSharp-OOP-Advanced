public class FileReadingFeedback : Feedback
{
    public override string BeginMessage => " Reading file(s)...";
    public override string ProgressMessage => " File(s) read successfully!";
    public override string EndMessage => " The specified file is empty.";
    public override string ResultMessage => " The specified file contains the following information:";
}
