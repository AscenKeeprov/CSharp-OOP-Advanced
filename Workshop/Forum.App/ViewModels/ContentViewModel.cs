namespace Forum.App.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Forum.App.Contracts.ViewModelContracts;

    public class ContentViewModel : IContentViewModel
    {
	private const int lineLength = 37;

	public string[] Content { get; }

	public ContentViewModel(string content)
	{
	    Content = GetLines(content);
	}

	private string[] GetLines(string content)
	{
	    char[] contentChars = content.ToCharArray();
	    ICollection<string> lines = new List<string>();
	    for (int i = 0; i < content.Length; i += lineLength)
	    {
		char[] row = contentChars.Skip(i).Take(lineLength).ToArray();
		string rowString = String.Join(String.Empty, row);
		lines.Add(rowString);
	    }
	    return lines.ToArray();
	}
    }
}
