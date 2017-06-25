using API.Interrogation.Querying;
using LinqToTwitter;
using System.Collections.Generic;
using System.Linq;

namespace Data.Interrogation
{
	public class TweetsByCountryQueryBuilder : IQueryBuilder
	{

		private string GetGeoID(TwitterContext context, string query)
		{
			string result = "Not Found";

			Geo geoSearch = context.Geo.SingleOrDefault(geo => geo.Type == GeoType.Search && geo.Query == query);

			if (geoSearch?.Places != null)
			{
				Place place = geoSearch.Places.First();
				result = place.ID;
			}

			return result;
		}

		public Search GetQuery(TwitterContext context, string filterParam)
		{
			string geoId = GetGeoID(context, filterParam);

			IEnumerable<Place> places = context
				.Geo
				.SingleOrDefault(geo => geo.Type == GeoType.ID && geo.ID == "5a110d312052166f")
				.Places;

			Place country = places.FirstOrDefault();

			Search result = null;

			if (country != null)
			{
				result = context
					.Search
					.SingleOrDefault(search => search.Type == SearchType.Search && search.GeoCode == country.ID);
			}

			return result;
		}
	}
}