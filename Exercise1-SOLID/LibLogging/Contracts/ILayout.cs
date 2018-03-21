public interface ILayout
{
    string Format { get; }

    string FormatMessage(IMessage message);
}
