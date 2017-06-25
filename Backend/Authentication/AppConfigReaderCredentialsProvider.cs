using API.Authentication;
using LinqToTwitter;
using System.Collections.Specialized;
using System.Configuration;

namespace Data.Authentication
{
	/// <summary>Credentials provider that reads from the Application configuration file.</summary>
	internal class AppConfigReaderCredentialsProvider : ICredentialsProvider
	{
		public ICredentialStore RetrieveCredentials() {

			NameValueCollection oAuthConfiguration = ConfigurationManager.GetSection("OAuthConfiguration") as NameValueCollection;

			ICredentialStore result = new SingleUserInMemoryCredentialStore()
			{
				ConsumerKey = oAuthConfiguration["ConsumerKey"],
				ConsumerSecret = oAuthConfiguration["ConsumerSecret"],
				AccessToken = oAuthConfiguration["AccessToken"],
				AccessTokenSecret = oAuthConfiguration["AccessTokenSecret"]
			};

			return result;
		}
	}
}
