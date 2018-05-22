using System;

/// <summary>Maintains program appearance.</summary>
public class UserInterface : IUserInterface
{
    private static int defaultWindowWidth;
    private const int applicationWindowWidth = 103;
    private static int defaultWindowHeight;
    private const int applicationWindowHeight = 39;
    private static string defaultTitle;
    private const string applicationTitle = "BASHSOFT";
    private static int defaultBufferWidth;
    private static int defaultBufferHeight;
    private const int applicationBufferMultiplier = 10;
    private static ConsoleColor defaultBackgroundColor;
    private const ConsoleColor applicationBackgroundColor = ConsoleColor.Black;
    private static ConsoleColor defaultForegroundColor;
    private const ConsoleColor applicationFeedbackColor = ConsoleColor.Cyan;
    private const ConsoleColor applicationForegroundColor = ConsoleColor.Green;
    private const ConsoleColor applicationExceptionColor = ConsoleColor.Red;

    public void Load()
    {
	BackupDefaultSettings();
	ApplyCustomSettings();
    }

    private void BackupDefaultSettings()
    {
	defaultWindowWidth = Console.WindowWidth;
	defaultWindowHeight = Console.WindowHeight;
	defaultTitle = Console.Title;
	defaultBufferWidth = Console.BufferWidth;
	defaultBufferHeight = Console.BufferHeight;
	defaultBackgroundColor = Console.BackgroundColor;
	defaultForegroundColor = Console.ForegroundColor;
    }

    private void ApplyCustomSettings()
    {
	Console.SetWindowSize(applicationWindowWidth, applicationWindowHeight);
	Console.Title = applicationTitle;
	Console.BufferWidth = applicationWindowWidth;
	Console.BufferHeight = applicationWindowHeight * applicationBufferMultiplier;
	Console.BackgroundColor = applicationBackgroundColor;
	Console.ForegroundColor = applicationForegroundColor;
    }

    public void FormatOutput(Type outputType)
    {
	switch (outputType.Name.ToUpper())
	{
	    case "EXCEPTION":
		Console.ForegroundColor = applicationExceptionColor;
		break;
	    case "FEEDBACK":
		Console.ForegroundColor = applicationFeedbackColor;
		break;
	}
    }

    public void ResetOutputFormat()
    {
	Console.BackgroundColor = applicationBackgroundColor;
	Console.ForegroundColor = applicationForegroundColor;
    }

    public void Unload()
    {
	RestoreDefaultSettings();
    }

    private void RestoreDefaultSettings()
    {
	Console.SetWindowSize(defaultWindowWidth, defaultWindowHeight);
	Console.Title = defaultTitle;
	Console.BufferWidth = defaultBufferWidth;
	Console.BufferHeight = defaultBufferHeight;
	Console.BackgroundColor = defaultBackgroundColor;
	Console.ForegroundColor = defaultForegroundColor;
    }
}
