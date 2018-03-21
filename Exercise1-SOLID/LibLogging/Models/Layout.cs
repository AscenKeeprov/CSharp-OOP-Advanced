using System;
using System.Globalization;

public abstract class Layout : ILayout
{
    public virtual string TimeStampFormat => "M/d/yyyy h:mm:ss tt";
    public abstract string Format { get; }

    public virtual string FormatMessage(IMessage message)
    {
	string timeStamp = message.TimeStamp.ToString(TimeStampFormat, CultureInfo.InvariantCulture);
	string reportLevel = message.ReportLevel.ToString();
	string output = String.Format(Format, timeStamp, reportLevel, message.Text);
	return output;
    }
}
