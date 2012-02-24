using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Data;

namespace JstorCrawl.Domain
{
    public abstract class Command : Entity
    {
        /// <summary>
        /// The summary of this command, e.g. "Search Jstor by Author"
        /// </summary>
        public abstract string Summary { get; }

        /// <summary>
        /// Implementation of this command
        /// </summary>
        /// <returns>Summary of results: Dois, Subjects, Creators found</returns>
        public abstract CommandResult Execute();
    }
}
