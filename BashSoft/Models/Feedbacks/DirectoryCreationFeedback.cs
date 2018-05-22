public class DirectoryCreationFeedback : Feedback
{
    public override string Message => " Directory \"{0}\" already exists in \"{1}\".";
    public override string ProgressMessage => " Directory \"{0}\" created in \"{1}\".";
}
