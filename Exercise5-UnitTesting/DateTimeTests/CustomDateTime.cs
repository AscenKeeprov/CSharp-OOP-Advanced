using System;

public class CustomDateTime : IDateTime
{
    public DateTime First { get { return DateTime.MinValue; } }
    public DateTime Last { get { return DateTime.MaxValue; } }
    public DateTime Now { get { return DateTime.Now; } }
}
