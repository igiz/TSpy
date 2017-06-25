using System;

namespace API.Logging
{
	/// <summary>A class representing a log entry.</summary>
	public class LogMessage : IComparable
	{
		#region Public Properties

		/// <summary>Gets the time of log entry.</summary>
		public DateTime Time { get; }

		/// <summary>Gets the log message.</summary>
		public string Message { get; }

		/// <summary>Gets the severity.</summary>
		public Severity Severity { get; }

		#endregion

		#region Public Constructors

		/// <summary>The main constructor.</summary>
		/// <param name="message">The message.</param>
		/// <param name="severity">The severity.</param>
		public LogMessage(string message, Severity severity)
		{
			Message = message;
			Severity = severity;
			Time = DateTime.Now;
		}

		#endregion

		#region Public Methods

		/// <summary>Compares for precedence.</summary>
		/// <param name="log">The log to compare to.</param>
		/// <returns></returns>
		public int CompareTo(object log)
		{
			LogMessage logMessage = (LogMessage) log;
			int result = Time.CompareTo(logMessage.Time);
			return result;
		}

		/// <summary>Returns string representation of this log message.</summary>
		public override string ToString()
		{
			return $"{Time} : {Message}";
		}

		#endregion
	}
}
