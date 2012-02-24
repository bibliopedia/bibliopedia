using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JstorCrawl.Domain.ValueObjects;

namespace JstorCrawl.Domain
{
    public abstract class SearchCommand : Command
    {
        protected abstract string JstorSearchCode { get; }

        public Uri GetSearchUri()
        {
            throw new NotImplementedException();
            return new Uri(JstorSearchCode);
        }

        /// <summary>
        /// Performs the specific search 
        /// </summary>
        /// <returns></returns>
        public override CommandResponse Execute(CrawlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
