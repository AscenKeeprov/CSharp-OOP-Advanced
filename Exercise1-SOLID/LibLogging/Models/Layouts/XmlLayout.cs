using System;

public sealed class XmlLayout : Layout
{
    public override string TimeStampFormat => "M/d/yyyy h:mm:ss tt";
    public override string Format => "<log>" + Environment.NewLine +
	"\t<date>{0}</date>" + Environment.NewLine +
	"\t<level>{1}</level>" + Environment.NewLine +
	"\t<message>{2}</message>" + Environment.NewLine + "</log>";
}
