public interface IAppender
{
    ILayout Layout { get; }
    EReportLevel ReportLevel { get; }

    void AppendMessage(IMessage message);
}
