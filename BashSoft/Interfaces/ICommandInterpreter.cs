public interface ICommandInterpreter
{
    void StartProcessingCommands();
    string InterpretCommand();
    string[] ParseInput(string input);
}
