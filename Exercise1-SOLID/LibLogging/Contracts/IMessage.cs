using System;

public interface IMessage
{
    DateTime TimeStamp { get; }
    EReportLevel ReportLevel { get; }
    string Text { get; }
}
