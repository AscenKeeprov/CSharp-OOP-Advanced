namespace WorkForce
{
    public class StartUp
    {
        public static void Main()
        {
	    ICompany company = new Company("Hobo Inc.");
	    company.BeginOperations();
	}
    }
}
