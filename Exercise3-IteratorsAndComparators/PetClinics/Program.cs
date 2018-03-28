using System;
using System.Collections.Generic;
using System.Linq;

namespace PetClinics
{
    class Program
    {
	static void Main()
	{
	    List<Pet> pets = new List<Pet>();
	    List<Clinic> clinics = new List<Clinic>();
	    int commandsCount = int.Parse(Console.ReadLine());
	    for (int c = 1; c <= commandsCount; c++)
	    {
		string[] commandArgs = Console.ReadLine().Split();
		string command = commandArgs[0];
		try
		{
		    switch (command.ToUpper())
		    {
			case "CREATE":
			    string objectType = commandArgs[1];
			    string[] objectInfo = commandArgs.Skip(2).ToArray();
			    if (objectType.ToUpper() == "CLINIC")
			    {
				ClinicFactory clinicFactory = new ClinicFactory();
				Clinic clinic = clinicFactory.CreateClinic(objectInfo);
				if (!clinics.Any(cl => cl.Name == clinic.Name)) clinics.Add(clinic);
			    }
			    if (objectType.ToUpper() == "PET")
			    {
				PetFactory petFactory = new PetFactory();
				Pet pet = petFactory.CreatePet(objectInfo);
				if (!pets.Any(p => p.CompareTo(pet) == 0)) pets.Add(pet);
			    }
			    break;
			case "ADD":
			    string petName = commandArgs[1];
			    Pet petToAdd = pets.SingleOrDefault(p => p.Name == petName);
			    if (petToAdd == null)
				throw new InvalidOperationException("Invalid Operation!");
			    string clinicName = commandArgs[2];
			    Clinic clinicToAddTo = clinics.SingleOrDefault(cl => cl.Name == clinicName);
			    if (clinicToAddTo == null)
				throw new InvalidOperationException("Invalid Operation!");
			    Console.WriteLine(clinicToAddTo.AccommodatePet(petToAdd));
			    break;
			case "RELEASE":
			    clinicName = commandArgs[1];
			    Clinic clinicToReleaseFrom = clinics.SingleOrDefault(cl => cl.Name == clinicName);
			    if (clinicToReleaseFrom == null)
				throw new InvalidOperationException("Invalid Operation!");
			    Console.WriteLine(clinicToReleaseFrom.ReleasePet());
			    break;
			case "HASEMPTYROOMS":
			    clinicName = commandArgs[1];
			    Clinic clinicToCheck = clinics.SingleOrDefault(cl => cl.Name == clinicName);
			    Console.WriteLine(clinicToCheck.HasEmptyRooms);
			    break;
			case "PRINT":
			    clinicName = commandArgs[1];
			    Clinic clinicToPrint = clinics.SingleOrDefault(cl => cl.Name == clinicName);
			    if (commandArgs.Length == 3)
			    {
				int roomNumber = int.Parse(commandArgs[2]);
				Console.WriteLine(clinicToPrint.PrintRoomInfo(roomNumber));
			    }
			    else Console.WriteLine(clinicToPrint);
			    break;
		    }
		}
		catch (Exception exception)
		{
		    Console.WriteLine(exception.Message);
		}
	    }
	}
    }
}
