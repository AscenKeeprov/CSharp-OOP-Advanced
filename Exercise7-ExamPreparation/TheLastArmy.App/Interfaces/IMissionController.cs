public interface IMissionController
{
    string PerformMission(IMission mission);
    void FailMissionsOnHold();
}
