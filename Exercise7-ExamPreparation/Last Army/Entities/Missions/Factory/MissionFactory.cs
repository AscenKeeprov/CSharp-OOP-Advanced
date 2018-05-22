using System;
using System.Linq;
using System.Reflection;

public class MissionFactory : IMissionFactory
{
    public IMission CreateMission(string missionType, double scoreToComplete)
    {
	Type typeOfMission = Assembly.GetCallingAssembly().GetTypes()
	    .SingleOrDefault(type => type.Name.Equals(missionType));
	if (typeOfMission == null) throw new ArgumentException(
	    String.Format(OutputMessages.MissingMissionType, missionType));
	if (!typeof(IMission).IsAssignableFrom(typeOfMission))
	    throw new InvalidOperationException(
		String.Format(OutputMessages.InvalidMissionType, missionType));
	IMission mission = (IMission)Activator.CreateInstance(typeOfMission, scoreToComplete);
	return mission;
    }
}
