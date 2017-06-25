using System;
using LinqToTwitter;

namespace API.Context
{
	/// <summary>A class representing a Tweet from the API.</summary>
	public class TweetContext : IComparable
	{
		/// <summary>Gets or sets the user.</summary>
		public User User { get; set; }

		/// <summary>Gets or sets the origin country.</summary>
		public string Country { get; set; }

		/// <summary>Gets or sets the tweet content.</summary>
		public string Tweet { get; set; }

		/// <summary>Gets or sets the time tweet was tweeted.</summary>
		public DateTime Time { get; set; }

		#region Public Methods

		/// <summary>Compares tweet precedence.</summary>
		/// <param name="tweet">The tweet to compare against.</param>
		/// <returns>The precedence.</returns>
		public int CompareTo(object tweet)
		{
			TweetContext tweetContext = (TweetContext) tweet;
			int result = Time.CompareTo(tweetContext.Time);
			return result;
		}

		#endregion
	}
}
