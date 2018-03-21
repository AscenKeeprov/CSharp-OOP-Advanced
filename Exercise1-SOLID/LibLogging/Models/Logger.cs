using System;
using System.Collections.Generic;

public class Logger : ILogger
{
    private IReadOnlyCollection<IAppender> appenders;

    public Logger(ICollection<IAppender> appenders)
    {
	this.appenders = (IReadOnlyCollection<IAppender>)appenders;
    }

    internal void BeginProcessingMessages()
    {
	using (MessageFactory messageFactory = new MessageFactory())
	{
	    string input;
	    while (!(input = Console.ReadLine()).ToUpper().Equals("END"))
	    {
		string[] messageArgs = input.Split('|');
		IMessage message = messageFactory.CreateMessage(messageArgs);
		LogMessage(message);
	    }
	}
    }

    public void LogMessage(IMessage message)
    {
	foreach (IAppender appender in appenders)
	{
	    if (appender.ReportLevel <= message.ReportLevel)
		appender.AppendMessage(message);
	}
    }
}
