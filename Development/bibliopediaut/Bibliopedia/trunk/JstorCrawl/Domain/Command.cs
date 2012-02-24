using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Data;
using JstorCrawl.Domain.ValueObjects;

namespace JstorCrawl.Domain
{
    public abstract class Command : Entity
    {
        /// <summary>
        /// The summary of this command, e.g. "Search Jstor by Author
        /// </summary>
        public virtual string Summary { get; protected set; }

        /// <summary>
        /// Implementation of this command
        /// </summary>
        /// <returns>All of the commands that need to be enqueued, e.g. "Download Work X, Y, Z ... for Author</returns>
        public abstract CommandResponse Execute(CrawlerContext crawler);
    }
}
