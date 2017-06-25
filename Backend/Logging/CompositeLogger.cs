using API.Logging;
using System.Collections.Generic;

namespace Data.Logging
{
	public class CompositeLogger : ILogger
	{
		private readonly HashSet<ILogger> loggers;

		public CompositeLogger(IEnumerable<ILogger> loggers){
			this.loggers = new HashSet<ILogger>(loggers);
		}

		public void LogMessage(LogMessage message)
		{
			foreach (ILogger logger in loggers)
			{
				logger.LogMessage(message);
			}
		}
	}
}
