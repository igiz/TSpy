using System.Threading;
using API.Logging;
using Data.Interrogation;
using Data.Logging;

namespace Data.Main
{
	public static class Startup
	{
		public static void Main(string[] args)
		{
			// Display the number of command line arguments:
			ILogger logger = new ConsoleLogger();
			var interrogator = new StreamingInterrogator(logger);
			interrogator.Connect();
			interrogator.Search("Hello");
			Thread.Sleep(100000);
		}
	}
}