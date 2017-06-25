using API.Logging;
using System;
using System.IO;

namespace Data.Logging
{
	public class FileLogger : ILogger
	{
		private readonly string logFileName;

		public FileLogger(string logFileName)
		{
			this.logFileName = logFileName;
		}

		public void LogMessage(LogMessage message)
		{
			string filePath = Path.Combine(Environment.CurrentDirectory, logFileName);
			using (StreamWriter writer = File.AppendText(filePath)) {
				writer.WriteLine(message);
			}
		}
	}
}
