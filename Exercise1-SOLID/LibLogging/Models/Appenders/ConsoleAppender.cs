using System;

public sealed class ConsoleAppender : Appender
{
    public ConsoleAppender(ILayout layout) : base(layout) { }

    public override void AppendMessage(IMessage message)
    {
	string output = Layout.FormatMessage(message);
	Console.WriteLine(output);
	MessagesAppended++;
    }
}
