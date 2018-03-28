public class Room
{
    public int Number { get; private set; }
    public Pet Patient { get; private set; }
    public bool Occupied => Patient != null;

    public Room(int number)
    {
	Number = number;
    }

    public void AddPatient(Pet patient)
    {
	Patient = patient;
    }

    public void ReleasePatient()
    {
	Patient = null;
    }

    public override string ToString()
    {
	if (Patient != null) return Patient.ToString();
	else return "Room empty";
    }
}
