using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class Controller
{
    //private IEnumerable<TypeInfo> libraryTypes => Assembly.GetExecutingAssembly().DefinedTypes;
    //private TypeInfo[] layoutTypes => libraryTypes.Where(t => t.BaseType == typeof(Layout)).ToArray();

    private ICollection<IAppender> appenders;
    //private ICollection<ILayout> layouts;
    private ILogFile logFile;
    public Logger Logger { get; private set; }

    public Controller()
    {
	appenders = new List<IAppender>();
	//layouts = new List<ILayout>();
	logFile = new LogFile();
    }

    internal void InitializeAppenders()
    {
	int appendersCount = int.Parse(Console.ReadLine());
	using (AppenderFactory appenderFactory = new AppenderFactory())
	{
	    for (int a = 1; a <= appendersCount; a++)
	    {
		IAppender appender = MakeAppender(appenderFactory);
		SaveAppender(appender);
	    }
	}
    }

    private IAppender MakeAppender(AppenderFactory appenderFactory)
    {
	string[] appenderInfo = Console.ReadLine().Split();
	IAppender appender = appenderFactory.CreateAppender(appenderInfo);
	if (appender is FileAppender fileAppender) fileAppender.File = logFile;
	return appender;
    }

    private void SaveAppender(IAppender appender)
    {
	if (AppenderExists(appender))
	    throw new DuplicateAppenderException();
	else appenders.Add(appender);
    }

    private bool AppenderExists(IAppender appender)
    {
	return appenders.Any(a => a.GetType().Name == appender.GetType().Name
	&& a.Layout.GetType().Name == appender.Layout.GetType().Name
	&& a.ReportLevel == appender.ReportLevel);
    }

    //   private void InitializeLayouts()
    //   {
    //using (LayoutFactory layoutFactory = new LayoutFactory())
    //{
    //    foreach (TypeInfo layoutInfo in layoutTypes)
    //    {
    //	string layoutType = layoutInfo.Name;
    //	ILayout layout = layoutFactory.CreateLayout(layoutType);
    //	layouts.Add(layout);
    //    }
    //}
    ////TODO: Or read layout data from a directory with saved templates?
    //   }

    internal void InitializeLogger()
    {
	Logger = new Logger(appenders);
    }

    internal void DisplayAppendersInfo()
    {
	foreach (IAppender appender in appenders)
	{
	    Console.WriteLine(appender);
	}
    }
}
