using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JstorCrawl.Domain
{
    public class LoggedSearch : StringEntity
    {
        public LoggedSearch()
        {
        }

        public LoggedSearch(SearchCommand search, CommandResult result)
        {
            Search = search;
            Result = result;
        }

        public virtual SearchCommand Search { get; protected set; }
        public virtual CommandResult Result { get; protected set; }

        public override object ComputeIdentity()
        {
            return Search.Summary;
        }
    }
}
