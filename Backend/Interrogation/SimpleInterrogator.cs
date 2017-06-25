using API.Authentication;
using API.Context;
using API.Interrogation;
using API.Interrogation.Querying;
using API.Logging;
using Data.Authentication;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Data;

namespace Data.Interrogation
{
	public class SimpleInterrogator : IInterrogator
	{
		#region Private Fields

		private readonly IDictionary<FilterBy, IQueryBuilder> filterToQueryBuilder = new Dictionary<FilterBy, IQueryBuilder>
		{
			{FilterBy.Tweet, new TweetsWithKeywordQueryBuilder()},
			{FilterBy.Country, new TweetsByCountryQueryBuilder()}
		};

		private bool disposed;

		#endregion Private Fields

		#region Non-Public Properties

		protected IQueryBuilder QueryBuilder { get; private set; }

		protected IAuthorizer Authorizer { get; private set; }

		#endregion Non-Public Properties

		#region Public Properties

		public ObservableCollection<TweetContext> Tweets { get; }

		public CancellationTokenSource CancellationTokenSource { get; protected set; }

		public bool Connected { get; private set; }

		public ILogger Logger { get; }

		#endregion Public Properties

		#region Public Constructors

		public SimpleInterrogator(ILogger logger)
		{
			Logger = logger;
			Tweets = new ObservableCollection<TweetContext>();
			BindingOperations.EnableCollectionSynchronization(Tweets, new object());
			CancellationTokenSource = new CancellationTokenSource();
		}

		#endregion

		#region Public Methods

		public virtual void Search(string keyword)
		{
			Tweets.Clear();
			try {
				using (TwitterContext context = new TwitterContext(Authorizer)) {
					Search searchResponse = QueryBuilder.GetQuery(context, keyword);
					searchResponse.Statuses.ForEach(AddStatus);
					Logger.LogMessage(new LogMessage($"Returned {searchResponse.Statuses.Count} results.", Severity.Information));
				}
			} catch (Exception ex) {
				Logger.LogMessage(ex.InnerException != null
					? new LogMessage(ex.InnerException.Message, Severity.Error)
					: new LogMessage(ex.Message, Severity.Error));
			}
		}

		

		/// <summary>Sets the filter for this interrogator.</summary>
		/// <param name="filter">The filter.</param>
		public void SetFilter(FilterBy filter)
		{
			IQueryBuilder queryBuilder;
			if (!filterToQueryBuilder.TryGetValue(filter, out queryBuilder)) {
				Logger.LogMessage(new LogMessage("This filter is not supported by this Interrogator", Severity.Error));
			}
			QueryBuilder = queryBuilder;
		}

		public void Cancel()
		{
			CancellationTokenSource.Cancel();
			CancellationTokenSource.Dispose();
			CancellationTokenSource = new CancellationTokenSource();
		}

		public virtual void Connect()
		{
			if (!Connected) {
				//Todo: Move to Unity?
				ICredentialsProvider credentialsProvider = new AppConfigReaderCredentialsProvider();
				ICredentialStore credentials = credentialsProvider.RetrieveCredentials();
				Authorizer = new SingleUserAuthorizer {CredentialStore = credentials};
				IAuthenticator authenticator = new DefaultAuthenticator(Logger);
				IAuthenticationResult authenticationResult = authenticator.Authenticate(Authorizer);
				Connected = authenticationResult.Success;
				Severity severity = Connected ? Severity.Information : Severity.Error;
				Logger.LogMessage(new LogMessage(authenticationResult.Message, severity));
			}
		}

		public virtual void Disconnect()
		{
			if (Connected) {
				Cancel();
				Authorizer = null;
				Connected = false;
				Logger.LogMessage(new LogMessage("Disconnected from API.", Severity.Warning));
			}
		}

		public void Dispose()
		{
			if (!disposed) {
				Dispose(true);
			}
		}

		#endregion

		#region Non-Public Methods

		protected void AddStatus(Status status)
		{
			TweetContext tweet = new TweetContext()
			{
				User = status.User,
				Tweet = status.Text,
				Time = status.CreatedAt,
				Country = status.Place.Country ?? "Unknown"
			};

			Tweets.Add(tweet);
		}

		private void Dispose(bool disposeManaged)
		{
			if (disposeManaged) {
				CancellationTokenSource.Dispose();
				Authorizer = null;
			}
			disposed = true;
		}

		#endregion
	}
}