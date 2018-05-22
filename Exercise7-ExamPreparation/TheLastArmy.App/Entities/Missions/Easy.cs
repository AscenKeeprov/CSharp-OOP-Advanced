public class Easy : Mission
{
    private const string EasyMissionName = "Suppression of civil rebellion";
    private const double EasyMissionEnduranceRequired = 20;
    private const double EasyMissionWearLevelDecrement = 30;

    public override string Name => EasyMissionName;
    public override double EnduranceRequired => EasyMissionEnduranceRequired;
    public override double WearLevelDecrement => EasyMissionWearLevelDecrement;

    public Easy(double scoreToComplete) : base(scoreToComplete) { }
}
