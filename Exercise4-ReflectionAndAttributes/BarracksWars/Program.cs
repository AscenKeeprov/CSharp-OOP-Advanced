namespace BarracksWars
{
    class Program
    {
        static void Main()
        {
	    IRepository repository = new UnitRepository();
	    IUnitFactory unitFactory = new UnitFactory();
	    CommandInterpreter commandInterpreter = new CommandInterpreter(repository, unitFactory);
	    IRunnable engine = new Engine(repository, unitFactory, commandInterpreter);
	    engine.Run();
	}
    }
}
