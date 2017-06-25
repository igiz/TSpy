using API.Logging;
using System;
using System.Collections.Generic;

namespace Data.Logging
{
	public class ConsoleLogger : ILogger
	{
		private readonly IReadOnlyDictionary<Severity, ConsoleColor> severityLogColor = new Dictionary<Severity, ConsoleColor>
		{
			{Severity.Information, ConsoleColor.Green},
			{Severity.Warning, ConsoleColor.Yellow},
			{Severity.Error, ConsoleColor.Red},
		};

		public void LogMessage(LogMessage message)
		{
			ConsoleColor logColor = severityLogColor[message.Severity];
			ConsoleExtensions.WriteLine($"{message.Time} : {message.Message}", logColor);
		}
	}
}