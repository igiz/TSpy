using LinqToTwitter;

namespace API.Authentication
{
	/// <summary>Interface classes implementing credentials provision.</summary>
	public interface ICredentialsProvider
	{
		#region Methods

		/// <summary>Retrieves the credentials to be used for authenticating to twitter API.</summary>
		/// <returns>The credentials store.</returns>
		ICredentialStore RetrieveCredentials();

		#endregion
	}
}
