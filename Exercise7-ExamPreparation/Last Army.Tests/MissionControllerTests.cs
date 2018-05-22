using System;
using NUnit.Framework;

[TestFixture]
public class MissionControllerTests
{
    private const int MaxMissionsOnHold = 3;

    private IArmy army;
    private IWarehouse warehouse;
    private MissionController missionController;

    [SetUp]
    public void Init()
    {
	army = new Army();
	army.AddSoldier(new Ranker("Hank", 21, 21, 100));
	warehouse = new Warehouse();
	warehouse.AddAmmunitions("Gun", 10);
	warehouse.AddAmmunitions("AutomaticMachine", 4);
	warehouse.AddAmmunitions("Helmet", 7);
	missionController = new MissionController(army, warehouse);
    }

    [Test]
    public void MissionCompleteMessage()
    {
	IMission mission = new Easy(10);
	string missionOutcome = missionController.PerformMission(mission).Trim();
	Assert.That(missionOutcome, Is.EqualTo(string.Format(
	    OutputMessages.MissionSuccessful, mission.Name).Trim()));
    }

    [Test]
    public void MissionCompleteCounter()
    {
	int successCounterStart = missionController.SuccessMissionCounter;
	IMission mission = new Easy(10);
	missionController.PerformMission(mission);
	Assert.That(missionController.SuccessMissionCounter, Is.EqualTo(successCounterStart + 1));
    }

    [Test]
    public void MissionOnHold()
    {
	IMission mission = new Medium(64);
	string missionOutcome = missionController.PerformMission(mission).Trim();
	Assert.That(missionOutcome, Is.EqualTo(string.Format(
	    OutputMessages.MissionOnHold, mission.Name).Trim()));
    }

    [Test]
    public void MissionFailedMessage()
    {
	IMission mission = new Hard(100);
	for (int i = 1; i <= MaxMissionsOnHold; i++) missionController.PerformMission(mission);
	string missionOutcome = missionController.PerformMission(mission).Trim()
	    .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
	Assert.That(missionOutcome, Is.EqualTo(string.Format(
	    OutputMessages.MissionDeclined, mission.Name).Trim()));
    }

    [Test]
    public void MissionFailedCounter()
    {
	int failureCounterStart = missionController.FailedMissionCounter;
	IMission mission = new Hard(100);
	for (int i = 1; i <= MaxMissionsOnHold + 1; i++) missionController.PerformMission(mission);
	Assert.That(missionController.FailedMissionCounter, Is.EqualTo(failureCounterStart + 1));
    }

    [Test]
    public void RetreatFailsMissionsOnHold()
    {
	IMission mission = new Hard(100);
	for (int i = 1; i <= MaxMissionsOnHold; i++)
	    missionController.PerformMission(mission);
	missionController.FailMissionsOnHold();
	Assert.That(missionController.FailedMissionCounter, Is.EqualTo(MaxMissionsOnHold));
    }
}
