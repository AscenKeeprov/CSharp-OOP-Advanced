using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>Processes console commands.</summary>
public class CommandInterpreter : ICommandInterpreter
{
    private IServiceProvider Server;
    private IInputOutputManager IOManager => (IOManager)Server.GetService(typeof(IInputOutputManager));
    private IFileSystemManager FSManager => (FSManager)Server.GetService(typeof(IFileSystemManager));
    private ICommandFactory CommandFactory => (CommandFactory)Server.GetService(typeof(ICommandFactory));
    private ICommandCache CommandCache => (CommandCache)Server.GetService(typeof(ICommandCache));

    public CommandInterpreter(IServiceProvider serviceProvider)
    {
	Server = serviceProvider;
    }

    public void StartProcessingCommands()
    {
	IOManager.Output(typeof(String), $" {FSManager.CurrentDirectory}> ");
	while (true)
	{
	    string input = InterpretCommand();
	    IOManager.OutputLine();
	    try
	    {
		string[] commandParameters = ParseInput(input);
		IExecutable command = CommandFactory.Create(commandParameters);
		CommandCache.Add((ICommand)command);
		command.Execute();
	    }
	    catch (Exception exception)
	    {
		IOManager.OutputLine(typeof(Exception), exception.Message);
	    }
	    IOManager.OutputLine();
	    IOManager.Output(typeof(String), $" {FSManager.CurrentDirectory}> ");
	}
    }

    public string InterpretCommand()
    {
	Tuple<int, int> origin = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
	StringBuilder input = new StringBuilder();
	int index = 0;
	ConsoleKeyInfo userInput;
	while (!(userInput = IOManager.InputKey()).Key.Equals(ConsoleKey.Enter))
	{
	    int startIndex = index;
	    int startLength = input.Length;
	    switch (userInput.Key)
	    {
		case ConsoleKey.Backspace:
		    if (index > 0)
		    {
			index--;
			for (int i = index; i < input.Length - 1; i++)
			{
			    input[i] = input[i + 1];
			}
			input.Length--;
		    }
		    break;
		case ConsoleKey.Delete:
		    if (index < input.Length) input.Remove(index, 1);
		    break;
		case ConsoleKey.UpArrow:
		    input.Clear();
		    input.Append(CommandCache.Previous());
		    break;
		case ConsoleKey.DownArrow:
		    input.Clear();
		    input.Append(CommandCache.Next());
		    break;
		case ConsoleKey.LeftArrow:
		    if (index > 0) index--;
		    break;
		case ConsoleKey.RightArrow:
		    if (index < input.Length) index++;
		    break;
		default:
		    if (Char.IsLetterOrDigit(userInput.KeyChar) ||
			Char.IsPunctuation(userInput.KeyChar) ||
			Char.IsWhiteSpace(userInput.KeyChar) ||
			Char.IsSymbol(userInput.KeyChar))
		    {
			input.Insert(index, userInput.KeyChar.ToString());
			index++;
		    }
		    break;
	    }
	    Console.SetCursorPosition(origin.Item1, origin.Item2);
	    IOManager.Output(typeof(String), new string(' ', startLength));
	    Console.SetCursorPosition(origin.Item1, origin.Item2);
	    IOManager.Output(typeof(String), input.ToString());
	    int horizontalOffset = (origin.Item1 + index) % Console.BufferWidth;
	    int verticalOffset = origin.Item2 + ((origin.Item1 + index) / Console.BufferWidth);
	    Console.SetCursorPosition(horizontalOffset, verticalOffset);
	}
	IOManager.OutputLine();
	return input.ToString();
    }

    public string[] ParseInput(string input)
    {
	string[] commandParameters = null;
	if (input.Contains('"'))
	{
	    string[] elements = null;
	    elements = Regex.Split(input.Trim('"'), @"\""?\s+\""|\""\s+\""?");
	    if (elements[0].Contains(' '))
	    {
		commandParameters = new string[elements.Length + 1];
		commandParameters[0] = elements[0].Split()[0];
		commandParameters[1] = elements[0].Split()[1];
		for (int e = 1; e < elements.Length; e++)
		    commandParameters[e + 1] = elements[e];
	    }
	    else commandParameters = elements;
	}
	else commandParameters = input.Trim()
	    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
	if (String.IsNullOrWhiteSpace(commandParameters[0]))
	    throw new MissingCommandException();
	return commandParameters;
    }
}
