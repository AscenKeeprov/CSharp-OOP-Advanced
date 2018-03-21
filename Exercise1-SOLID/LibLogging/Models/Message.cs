using System;

public class Message : IMessage
{
    public DateTime TimeStamp { get; }
    public EReportLevel ReportLevel { get; }
    public string Text { get; }

    public Message(EReportLevel reportLevel, DateTime timeStamp, string text)
    {
	ReportLevel = reportLevel;
	TimeStamp = timeStamp;
	Text = text;
    }
}
