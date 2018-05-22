using System;

public interface IUserInterface
{
    void Load();
    void FormatOutput(Type outputType);
    void ResetOutputFormat();
    void Unload();
}
