namespace API.Authentication
{
	/// <summary>The interface for classes providing authentication results.</summary>
	public interface IAuthenticationResult
	{
		#region Properties

		/// <summary>Gets flag indicating whether authentication was a success.</summary>
		bool Success { get; }

		/// <summary>Gets the result message if any.</summary>
		string Message { get; }

		#endregion
	}
}
