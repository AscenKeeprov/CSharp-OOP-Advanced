public abstract class Feedback
{
    public virtual string Message { get; }
    public virtual string BeginMessage { get; }
    public virtual string ProgressMessage { get; }
    public virtual string AbortMessage { get; }
    public virtual string EndMessage { get; }
    public virtual string ResultMessage { get; }
}
