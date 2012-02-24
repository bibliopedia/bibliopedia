using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor.Domain;
using Iesi.Collections.Generic;
using FluentNHibernate.Data;

namespace JstorCrawl.Domain
{
    public class CrawlerContext : Entity
    {
        public CrawlerContext()
        {
            CommandQueue = new SynchronizedCollection<Command>();

            CrawledCreators = new SynchronizedSet<string>(new HashedSet<string>());
            CrawledIdentifiers = new SynchronizedSet<string>(new HashedSet<string>());
        }

        /// <summary>
        /// Executes commands one at a time
        /// </summary>
        /// <returns>A list of summaries of results of the execution of the commands</returns>
        public virtual IEnumerator<string> CommandEnumerator()
        {
            while (CommandQueue.Count() > 0)
            {
                using (var enumerator = CommandQueue.GetEnumerator())
                {
                    var result = enumerator.Current.Execute(this);

                    CommandQueue.Concat(result.CommandsToEnqueue);

                    yield return result.SummaryOfResults;
                }

            }
        }

        /// <summary>
        /// Should be a priority queue where subjects then authors then works with more authors bubble to top
        /// </summary>
        public virtual IEnumerable<Command> CommandQueue { get; protected set; }

        public virtual ISet<string> CrawledCreators { get; protected set; }
        public virtual ISet<string> CrawledIdentifiers { get; protected set; }
    }
}
