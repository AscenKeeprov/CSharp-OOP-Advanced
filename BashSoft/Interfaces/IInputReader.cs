using System;

public interface IInputReader
{
    int Input();
    string InputLine();
    ConsoleKeyInfo InputKey();
}
