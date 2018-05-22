using System;

public class CommandCache : ICommandCache
{
    private const int CacheSize = 20;

    private ICommand[] commands;
    private int FirstEmptyCell => Array.FindIndex(commands, c => c == null);
    private int LastFilledCell => Array.FindLastIndex(commands, c => c != null);
    private int currentCell;

    public CommandCache()
    {
	commands = new ICommand[CacheSize];
    }

    public void Add(ICommand command)
    {
	if (FirstEmptyCell == -1) Update(command);
	else
	{
	    if (AlreadyAdded(command)) Relocate(command);
	    else commands[FirstEmptyCell] = command;
	}
	currentCell = LastFilledCell + 1;
    }

    private bool AlreadyAdded(ICommand command)
    {
	for (int c = 0; c < LastFilledCell; c++)
	{
	    if (commands[c].ToString().Equals(command.ToString()))
		return true;
	}
	return false;
    }

    private void Update(ICommand command)
    {
	if (AlreadyAdded(command)) Relocate(command);
	else ShiftLeft(0, command);
	currentCell = LastFilledCell + 1;
    }

    private void Relocate(ICommand command)
    {
	int commandCell = Array.FindIndex(commands,
	    c => c.ToString().Equals(command.ToString()));
	ShiftLeft(commandCell, command);
    }

    private void ShiftLeft(int startCell, ICommand lastCommand)
    {
	for (int c = startCell; c < LastFilledCell; c++)
	{
	    commands[c] = commands[c + 1];
	}
	commands[LastFilledCell] = lastCommand;
    }

    public string Previous()
    {
	if (currentCell > 0) currentCell--;
	return Read();
    }

    public string Next()
    {
	if (currentCell < LastFilledCell) currentCell++;
	return Read();
    }

    private string Read()
    {
	if (currentCell < 0 || currentCell >= commands.Length)
	    return String.Empty;
	ICommand command = commands[currentCell];
	if (command == null) return String.Empty;
	return command.ToString();
    }
}
