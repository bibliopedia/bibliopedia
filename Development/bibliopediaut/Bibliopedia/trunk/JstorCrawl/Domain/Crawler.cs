using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor.Domain;
using Iesi.Collections.Generic;
using FluentNHibernate.Data;

namespace JstorCrawl.Domain
{
    public class Crawler : Entity
    {
        public Crawler()
        {
            Context = new CrawlerContext();
        }

        public virtual void Crawl()
        {
            using(var enumerator = Context.CommandEnumerator())
            while(enumerator.Current != null)
            {
                Console.WriteLine(enumerator.Current);
            }
        }

        public virtual void EnqueueAuthorSearch(string authorName)
        {
            Context.CommandQueue.Concat(new [] {new SearchAuthor(authorName)});
        }

        public virtual void EnqueueSubjectSearch(string subjectName)
        {
            Context.CommandQueue.Concat(new [] {new SearchSubject(subjectName)});
        }

        public virtual CrawlerContext Context { get; protected set; }
    }
}
