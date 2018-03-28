using System;
using System.Linq;
using System.Text;

public class Clinic
{
    public string Name { get; private set; }
    private Room[] rooms;
    private Room centralRoom => rooms[(rooms.Length / 2)];
    public bool HasEmptyRooms => rooms.Any(r => r.Occupied == false);
    private bool HasOccupiedRooms => rooms.Any(r => r.Occupied == true);

    public Clinic(string name, int roomsCount)
    {
	Name = name;
	rooms = new Room[roomsCount];
	for (int r = 0; r < rooms.Length; r++)
	    rooms[r] = new Room(r + 1);
    }

    public bool AccommodatePet(Pet pet)
    {
	if (!HasEmptyRooms) return false;
	int currentRoomNumber = centralRoom.Number - 1;
	for (int i = 0; i < rooms.Length; i++)
	{
	    if (i % 2 == 0) currentRoomNumber += i;
	    else currentRoomNumber -= i;
	    Room currentRoom = rooms[currentRoomNumber];
	    if (currentRoom.Occupied == false)
	    {
		currentRoom.AddPatient(pet);
		return true;
	    }
	}
	return false;
    }

    public bool ReleasePet()
    {
	if (!HasOccupiedRooms) return false;
	for (int i = 0; i < rooms.Length; i++)
	{
	    int currentRoomNumber = (centralRoom.Number - 1 + i) % rooms.Length;
	    Room currentRoom = rooms[currentRoomNumber];
	    if (currentRoom.Occupied == true)
	    {
		currentRoom.ReleasePatient();
		return true;
	    }
	}
	return false;
    }

    public string PrintRoomInfo(int roomNumber)
    {
	if (roomNumber < 0 || roomNumber >= rooms.Length)
	    throw new InvalidOperationException("Invalid Operation!");
	Room room = rooms.Single(r => r.Number == roomNumber);
	return room.ToString();
    }

    public override string ToString()
    {
	StringBuilder clinicInfo = new StringBuilder();
	foreach (Room room in rooms)
	    clinicInfo.AppendLine(room.ToString());
	return clinicInfo.ToString().TrimEnd();
    }
}
