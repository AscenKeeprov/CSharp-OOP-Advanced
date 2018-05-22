namespace Forum.App.Contracts.FactoryContracts
{
    using Forum.App.Models;
    using ModelContracts;

    public interface ILabelFactory
    {
	ILabel CreateLabel(string content, Position position, bool isHidden = false);
	IButton CreateButton(string content, Position position, bool isHidden = false, bool isField = false);
    }
}
