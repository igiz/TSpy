using API.Authentication;

namespace Data.Authentication
{
	internal class AuthenticationResult : IAuthenticationResult
	{
		public bool Success { get; }

		public string Message { get; }

		public AuthenticationResult(bool success, string message)
		{
			Success = success;
			Message = message;
		}
	}
}
