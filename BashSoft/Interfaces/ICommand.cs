public interface ICommand : IValidatable, IExecutable
{
    string[] Parameters { get; }
}
