using API.Logging;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Interrogation
{
	public class StreamingInterrogator : SimpleInterrogator
	{
		public StreamingInterrogator(ILogger logger) : base(logger){ }

		public override async void Search(string keyword)
		{
			try{
				using (TwitterContext context = new TwitterContext(Authorizer)) {
					Cancel();
					Tweets.Clear();
					Logger.LogMessage(new LogMessage($"Streaming has begun for keyword: {keyword}.", Severity.Information));

					IQueryable<Streaming> streamQuery = context.Streaming
						.Where(strm => strm.Type == StreamingType.Filter && strm.Track == keyword);

					Task<List<Streaming>> stream = streamQuery
						.WithCancellation(CancellationTokenSource.Token)
						.StartAsync(content => Task.Run(() => {
							if (!string.IsNullOrEmpty(content.Content)) {
								Status status = content.Entity as Status;
								if (status != null) {
									AddStatus(status);
								}
							}
						}));

					await stream;
				}
			} catch (OperationCanceledException) {
				Logger.LogMessage(new LogMessage($"Stream for '{keyword}' has stopped.", Severity.Information));
			} catch (Exception ex) {
				Logger.LogMessage(new LogMessage(ex.Message, Severity.Error));
			}
		}
	}
}