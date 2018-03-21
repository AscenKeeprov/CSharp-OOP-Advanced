using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

public abstract class Factory : IDisposable
{
    bool disposed = false;
    SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

    public void Dispose()
    {
	Dispose(true);
	GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
	if (disposed) return;
	if (disposing)
	{
	    if (handle != null) handle.Dispose();
	}
	disposed = true;
    }

    ~Factory()
    {
	Dispose(false);
    }
}
