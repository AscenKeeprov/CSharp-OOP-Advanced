using System;
using System.Linq;
using System.Text;

public class GameController : IGameController
{
    private ISoldierFactory soldierFactory;
    private IMissionFactory missionFactory;
    private StringBuilder missionResults;

    public IArmy Army { get; }
    public IWarehouse Warehouse { get; }
    public MissionController MissionController { get; }

    public GameController()
    {
	Army = new Army();
	Warehouse = new Warehouse();
	MissionController = new MissionController(Army, Warehouse);
	soldierFactory = new SoldierFactory();
	missionFactory = new MissionFactory();
	missionResults = new StringBuilder();
    }

    public void GiveInputToGameController(string input)
    {
	string[] data = input.Split();
	if (data[0].Equals("Soldier"))
	{
	    string soldierType = data[1];
	    string soldierName = data[2];
	    int soldierAge = int.Parse(data[3]);
	    double soldierExperience = double.Parse(data[4]);
	    double soldierEndurance = double.Parse(data[5]);
	    ISoldier soldier = soldierFactory.CreateSoldier(soldierType, soldierName, soldierAge, soldierExperience, soldierEndurance);
	    if (!Warehouse.TryEquipSoldier(soldier)) missionResults.AppendLine(
		    String.Format(OutputMessages.WeaponNotAvailable, soldierType, soldierName));
	    else Army.AddSoldier(soldier);
	}
	else if (data[0].Equals("WareHouse"))
	{
	    string ammunitionType = data[1];
	    int ammunitionQuantity = int.Parse(data[2]);
	    Warehouse.AddAmmunitions(ammunitionType, ammunitionQuantity);
	}
	else if (data[0].Equals("Mission"))
	{
	    string missionType = data[1];
	    double scoreToComplete = double.Parse(data[2]);
	    IMission mission = missionFactory.CreateMission(missionType, scoreToComplete);
	    string missionOutcome = MissionController.PerformMission(mission).TrimEnd();
	    missionResults.AppendLine(missionOutcome);
	}
    }

    public string RequestResult()
    {
	MissionController.FailMissionsOnHold();
	missionResults.AppendLine("Results:");
	missionResults.AppendLine($"Successful missions - {MissionController.SuccessMissionCounter}");
	missionResults.AppendLine($"Failed missions - {MissionController.FailedMissionCounter}");
	missionResults.AppendLine("Soldiers:");
	foreach (ISoldier soldier in Army.Soldiers.OrderByDescending(s => s.OverallSkill))
	{
	    missionResults.AppendLine(soldier.ToString());
	}
	return missionResults.ToString().TrimEnd();
    }
}
