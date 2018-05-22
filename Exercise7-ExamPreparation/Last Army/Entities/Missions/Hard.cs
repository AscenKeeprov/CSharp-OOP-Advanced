public class Hard : Mission
{
    private const string HardMissionName = "Disposal of terrorists";
    private const double HardMissionEnduranceRequired = 80;
    private const double HardMissionWearLevelDecrement = 70;

    public override string Name => HardMissionName;
    public override double EnduranceRequired => HardMissionEnduranceRequired;
    public override double WearLevelDecrement => HardMissionWearLevelDecrement;

    public Hard(double scoreToComplete) : base(scoreToComplete) { }
}
