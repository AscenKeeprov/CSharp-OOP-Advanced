using System;

public class FileAlreadyDownloadedException : Exception
{
    public override string Message => " The requested file already exists in the current folder." +
	Environment.NewLine + " Do you want to overwrite it? (Y/N) ";
}
