using System;

public interface IDateTime
{
    DateTime First { get; }
    DateTime Last { get; }
    DateTime Now { get; }
}
