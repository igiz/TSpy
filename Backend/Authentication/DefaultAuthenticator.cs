using API.Authentication;
using API.Logging;
using LinqToTwitter;
using System;
using System.Linq;

namespace Data.Authentication
{
	internal class DefaultAuthenticator : IAuthenticator
	{

		#region Non-Public Constants

		private const int BadAuthenticationDataErrorCode = 215;

		#endregion

		#region Non-Public Properties

		private ILogger Logger { get; set; }

		#endregion

		#region Public Constructors

		public DefaultAuthenticator(ILogger logger)
		{
			Logger = logger;
		}

		#endregion

		public IAuthenticationResult Authenticate(IAuthorizer authorizer)
		{
			IAuthenticationResult result;
			authorizer.AuthorizeAsync();
			TwitterContext twitterContext = new TwitterContext(authorizer);

			try{
				Account account = twitterContext.Account.SingleOrDefault(acc => acc.Type == AccountType.VerifyCredentials);
				result = new AuthenticationResult(true, $"Successfully authenticated to twitter API as user : {account.User.Name} .");
			} catch (Exception exception) {
				if (exception.InnerException?.GetType() == typeof(TwitterQueryException)){
					result = ((TwitterQueryException) exception.InnerException).ErrorCode == BadAuthenticationDataErrorCode ? 
						new AuthenticationResult(false, "Unable to twitter API. Check credential details.") : 
						new AuthenticationResult(false, $"Unknown error was encountered:\n {exception.Message}");
				} else {
					throw;
				}
			}
			Logger.LogMessage(new LogMessage($"Authentication {(result.Success ? "succesfull" : "failed")}.", Severity.Information));
			return result;
		}
	}
}
