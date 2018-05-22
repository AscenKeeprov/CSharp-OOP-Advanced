using System;

namespace WebSite
{
    public class StartUp
    {
        public static void Main()
        {
	    //TODO: IS THIS NECESSARY AT ALL ??? SERVICES ???
	    //IServer server = new Server();
	    //server.Start();
	    ISite webSite = new Site();
	    webSite.BeginProcessingRequests();
        }
    }
}
