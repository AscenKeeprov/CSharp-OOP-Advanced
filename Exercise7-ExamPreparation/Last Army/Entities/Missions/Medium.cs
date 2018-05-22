public class Medium : Mission
{
    private const string MediumName = "Capturing dangerous criminals";
    private const double MediumEnduranceRequired = 50;
    private const double MediumWearLevelDecrement = 50;

    public override string Name => MediumName;
    public override double EnduranceRequired => MediumEnduranceRequired;
    public override double WearLevelDecrement => MediumWearLevelDecrement;

    public Medium(double scoreToComplete) : base(scoreToComplete) { }
}
