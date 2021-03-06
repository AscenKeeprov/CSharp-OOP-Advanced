﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MissionController : IMissionController
{
    private IArmy army;
    private IWarehouse wareHouse;
    private Queue<IMission> missionQueue;

    public Queue<IMission> Missions => this.missionQueue;
    public int SuccessMissionCounter { get; private set; }
    public int FailedMissionCounter { get; private set; }

    public MissionController(IArmy army, IWarehouse wareHouse)
    {
	this.army = army;
	this.wareHouse = wareHouse;
	this.missionQueue = new Queue<IMission>();
    }

    public string PerformMission(IMission mission)
    {
	StringBuilder sb = new StringBuilder();
	if (this.missionQueue.Count >= 3)
	{
	    sb.AppendLine(string.Format(OutputMessages.MissionDeclined, this.missionQueue.Dequeue().Name));
	    this.FailedMissionCounter++;
	}
	this.missionQueue.Enqueue(mission);
	int currentMissionsCount = this.missionQueue.Count;
	for (int i = 0; i < currentMissionsCount; i++)
	{
	    this.wareHouse.TryEquipArmy(this.army);
	    IMission currentMission = this.missionQueue.Dequeue();
	    List<ISoldier> missionTeam = this.CollectMissionTeam(mission);
	    bool successful = this.ExecuteMission(currentMission, missionTeam);
	    if (successful) sb.AppendLine(string.Format(
		OutputMessages.MissionSuccessful, currentMission.Name));
	    else
	    {
		this.missionQueue.Enqueue(currentMission);
		sb.AppendLine(string.Format(OutputMessages.MissionOnHold, currentMission.Name));
	    }
	}
	return sb.ToString();
    }

    private List<ISoldier> CollectMissionTeam(IMission mission)
    {
	List<ISoldier> missionTeam = this.army.Soldiers
	    .Where(s => s.ReadyForMission(mission)).ToList();
	return missionTeam;
    }

    private bool ExecuteMission(IMission mission, List<ISoldier> missionTeam)
    {
	if (missionTeam.Sum(s => s.OverallSkill) >= mission.ScoreToComplete)
	{
	    foreach (ISoldier soldier in missionTeam)
	    {
		soldier.CompleteMission(mission);
	    }
	    this.SuccessMissionCounter++;
	    return true;
	}
	return false;
    }

    public void FailMissionsOnHold()
    {
	while (this.missionQueue.Count > 0)
	{
	    this.FailedMissionCounter++;
	    this.missionQueue.Dequeue();
	}
    }
}