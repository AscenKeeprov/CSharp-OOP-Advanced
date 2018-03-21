using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

public class MessageFactory : Factory
{
    bool disposed = false;
    SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

    public IMessage CreateMessage(string[] messageArgs)
    {
	if (disposed) throw new ObjectDisposedException(GetType().Name);
	EReportLevel reportLevel = EReportLevel.INFO;
	if (!Enum.TryParse(typeof(EReportLevel), messageArgs[0], true, out object threshold))
	    throw new InvalidThresholdException();
	else reportLevel = (EReportLevel)threshold;
	string timeStampFormat = "M/d/yyyy h:mm:ss tt";
	DateTime timeStamp = DateTime.ParseExact(messageArgs[1], timeStampFormat, CultureInfo.InvariantCulture);
	string text = messageArgs[2];
	IMessage message = new Message(reportLevel, timeStamp, text);
	return message;
    }

    protected override void Dispose(bool disposing)
    {
	if (disposed) return;
	if (disposing)
	{
	    if (handle != null) handle.Dispose();
	}
	disposed = true;
	base.Dispose(disposing);
    }

    ~MessageFactory()
    {
	Dispose(false);
    }
}
