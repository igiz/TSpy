using LinqToTwitter;

namespace API.Authentication
{
	/// <summary>The interface for authenticator classes that handle logging into Twitter API.</summary>
	public interface IAuthenticator
	{
		#region Methods

		/// <summary>Authenticates the login into Twitter using the given <see cref="IAuthorizer"/> instance.</summary>
		/// <param name="authorizer">The authorizer.</param>
		/// <returns>An instance of <see cref="IAuthenticationResult"/> indicating the success of authorization.</returns>
		IAuthenticationResult Authenticate(IAuthorizer authorizer);

		#endregion
	}
}
