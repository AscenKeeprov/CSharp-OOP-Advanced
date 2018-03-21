using System.Text;

public abstract class Appender : IAppender
{
    public ILayout Layout { get; }
    public EReportLevel ReportLevel { get; private set; }
    public int MessagesAppended { get; protected set; }

    protected Appender(ILayout layout)
    {
	Layout = layout;
    }

    public void SetReportLevel(EReportLevel reportLevel)
    {
	ReportLevel = reportLevel;
    }

    public abstract void AppendMessage(IMessage message);

    public override string ToString()
    {
	StringBuilder appenderInfo = new StringBuilder();
	appenderInfo.Append($"Appender type: {GetType().Name}");
	appenderInfo.Append($", Layout type: {Layout.GetType().Name}");
	appenderInfo.Append($", Report level: {ReportLevel.ToString()}");
	appenderInfo.Append($", Messages appended: {MessagesAppended}");
	return appenderInfo.ToString().TrimEnd();
    }
}
