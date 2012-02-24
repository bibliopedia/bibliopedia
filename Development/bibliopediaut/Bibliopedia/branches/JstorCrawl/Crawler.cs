using System;
using FluentNHibernate.Data;
using JstorCrawl.Domain;

namespace JstorCrawl
{
    public class Crawler : Entity
    {
        public Crawler()
        {
            Context = new CrawlerContext();
        }

        public virtual void Crawl()
        {
            foreach (var result in Context.Results())
            {

            }
        }

        public virtual CrawlerContext Context { get; protected set; }
    }
}
