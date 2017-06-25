using API.Interrogation.Querying;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interrogation
{
    public class TweetsWithKeywordQueryBuilder : IQueryBuilder
    {
        public Search GetQuery(TwitterContext context, string filterParam)
        {
            Search result = context
                .Search
                .SingleOrDefault(search => search.Type == SearchType.Search && search.Query == filterParam);

            return result;
        }
    }
}
