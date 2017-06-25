using LinqToTwitter;

namespace API.Interrogation.Querying
{
	/// <summary>Interface for classes implementing query building logic.</summary>
	public interface IQueryBuilder
	{
		#region Public Methods

		/// <summary>Builds the query and returns instance of <see cref="Search"/>.</summary>
		/// <param name="context">The context.</param>
		/// <param name="filterParam">The filter parameter.</param>
		/// <returns>The instance of <see cref="Search"/>.</returns>
		Search GetQuery(TwitterContext context, string filterParam);

		#endregion
	}
}
