using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

public class AppenderFactory : Factory
{
    bool disposed = false;
    SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

    public IAppender CreateAppender(string[] appenderInfo)
    {
	if (disposed) throw new ObjectDisposedException(GetType().Name);
	Type appenderType = Type.GetType(appenderInfo[0], false, true);
	if (appenderType == null) throw new InvalidAppenderException();
	ILayout layout = null;
	using (LayoutFactory layoutFactory = new LayoutFactory())
	{
	    layout = layoutFactory.CreateLayout(appenderInfo[1]);
	}
	Appender appender = (Appender)Activator.CreateInstance(appenderType, layout);
	appender.SetReportLevel(EReportLevel.INFO);
	if (appenderInfo.Length == 3)
	{
	    if (!Enum.TryParse(typeof(EReportLevel), appenderInfo[2], true, out object threshold))
		throw new InvalidThresholdException();
	    else appender.SetReportLevel((EReportLevel)threshold);
	}
	return appender;
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

    ~AppenderFactory()
    {
	Dispose(false);
    }
}
