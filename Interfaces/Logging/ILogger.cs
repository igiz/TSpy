namespace API.Logging
{
	/// <summary>Interface for classes implementing logging functionality.</summary>
	public interface ILogger
	{
		#region Methods

		/// <summary>Logs a given message.</summary>
		/// <param name="message">The log message.</param>
		void LogMessage(LogMessage message);

		#endregion
	}
}