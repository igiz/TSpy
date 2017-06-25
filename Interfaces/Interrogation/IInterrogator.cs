using System;
using API.Logging;
using API.Context;
using System.Collections.ObjectModel;
using System.Threading;

namespace API.Interrogation
{
	public interface IInterrogator : IDisposable
	{
		#region Properties

		/// <summary>Gets the flag indicating whether the interrogator is connected to the API.</summary>
		bool Connected { get; }

		/// <summary>Gets the logger used by this interrogator.</summary>
		ILogger Logger { get; }

		/// <summary>Observable collection of tweets.</summary>
		ObservableCollection<TweetContext> Tweets { get; }

		/// <summary>The cancellation token source.</summary>
		CancellationTokenSource CancellationTokenSource { get; }

		#endregion

		#region Methods

		/// <summary>Connects the interrogator to the API.</summary>
		void Connect();

		/// <summary>Disconnects the interrogator from the API.</summary>
		void Disconnect();

		/// <summary>Searches for tweets given the keyword in the status.</summary>
		/// <param name="keyword">The keyword.</param>
		/// <returns>An <see cref="IEnumerable"/> matching the keyword.</returns>
		void Search(string keyword);

		/// <summary>Sets the filter for this interrogator.</summary>
		/// <param name="filter">The filter.</param>
		void SetFilter(FilterBy filter);

		/// <summary>Cancels the current interrogation.</summary>
		void Cancel();

		#endregion
	}
}