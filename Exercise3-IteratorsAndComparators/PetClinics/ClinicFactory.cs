using System;

public class ClinicFactory
{
    internal Clinic CreateClinic(string[] clinicInfo)
    {
	string name = clinicInfo[0];
	int roomsCount = int.Parse(clinicInfo[1]);
	if (roomsCount % 2 == 0)
	    throw new InvalidOperationException("Invalid Operation!");
	Clinic clinic = new Clinic(name, roomsCount);
	return clinic;
    }
}
