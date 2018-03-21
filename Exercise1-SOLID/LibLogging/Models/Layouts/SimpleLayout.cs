public sealed class SimpleLayout : Layout
{
    public override string TimeStampFormat => "M/d/yyyy h:mm:ss tt";
    public override string Format => "{0} - {1} - {2}";
}
