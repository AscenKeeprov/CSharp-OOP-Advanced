namespace Forum.App.Contracts.ModelContracts
{
    public interface ILabel : IPositionable
    {
	string Text { get; }

	bool IsHidden { get; }
    }
}