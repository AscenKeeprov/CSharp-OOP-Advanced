using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

public class LayoutFactory : Factory
{
    bool disposed = false;
    SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

    public ILayout CreateLayout(string layoutType)
    {
	if (disposed) throw new ObjectDisposedException(GetType().Name);
	Type typeOfLayout = Type.GetType(layoutType, false, true);
	if (typeOfLayout == null) throw new InvalidLayoutException();
	ILayout layout = (ILayout)Activator.CreateInstance(typeOfLayout);
	return layout;
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

    ~LayoutFactory()
    {
	Dispose(false);
    }
}
