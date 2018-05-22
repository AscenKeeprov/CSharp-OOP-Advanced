using System;

namespace KingsGambit
{
    public class StartUp
    {
	public static void Main()
	{
	    string kingName = Console.ReadLine();
	    IKing king = new King(kingName);
	    MusterTroops(king);
	    string input;
	    while (!(input = Console.ReadLine()).Equals("End"))
	    {
		string[] eventArgs = input.Split();
		string eventType = eventArgs[0];
		switch (eventType.ToUpper())
		{
		    case "ATTACK":
			king.ReportAttack();
			break;
		    case "KILL":
			string defenderName = eventArgs[1];
			IDefender defender = king.CallDefender(defenderName);
			defender.TakeDamage();
			break;
		}
	    }
	}

	private static void MusterTroops(IKing king)
	{
	    string[] royalGuardsNames = Console.ReadLine().Split();
	    foreach (string royalGuardName in royalGuardsNames)
	    {
		IDefender defender = new RoyalGuard(royalGuardName);
		king.HireDefender(defender);
	    }
	    string[] footmenNames = Console.ReadLine().Split();
	    foreach (string footmanName in footmenNames)
	    {
		IDefender defender = new Footman(footmanName);
		king.HireDefender(defender);
	    }
	}
    }
}
