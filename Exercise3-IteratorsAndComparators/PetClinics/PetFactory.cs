public class PetFactory
{
    internal Pet CreatePet(string[] petInfo)
    {
	string name = petInfo[0];
	int age = int.Parse(petInfo[1]);
	string kind = petInfo[2];
	Pet pet = new Pet(name, age, kind);
	return pet;
    }
}
