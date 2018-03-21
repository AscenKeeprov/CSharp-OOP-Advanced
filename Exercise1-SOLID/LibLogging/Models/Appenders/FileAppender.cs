public sealed class FileAppender : Appender
{
    public ILogFile File { get; set; }

    public FileAppender(ILayout layout) : base(layout) { }

    public override void AppendMessage(IMessage message)
    {
	string output = Layout.FormatMessage(message);
	File.Write(output);
	MessagesAppended++;
    }

    public override string ToString()
    {
	return base.ToString() + $", File size: {File.Size}";
    }
}
