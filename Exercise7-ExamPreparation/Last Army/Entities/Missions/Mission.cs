public abstract class Mission : IMission
{
    public abstract string Name { get; }
    public abstract double EnduranceRequired { get; }
    public abstract double WearLevelDecrement { get; }
    public double ScoreToComplete { get; }

    protected Mission(double scoreToComplete)
    {
	ScoreToComplete = scoreToComplete;
    }
}
