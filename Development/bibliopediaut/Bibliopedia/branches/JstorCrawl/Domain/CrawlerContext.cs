using System;
using System.Text;
using Iesi.Collections.Generic;
//using System.Collections.Generic;
using System.Linq;
using Jstor.Domain;
using FluentNHibernate.Data;
using Data;

namespace JstorCrawl.Domain
{
    public class CrawlerContext : Entity
    {
        public CrawlerContext()
        {
            CommandQueue = new EntityQueue<Command>();
        }

        public CrawlerContext(string name)
            : this()
        {
            Name = name;
        }

        public virtual string Name { get; protected set; }


        public virtual EntityQueue<Command> CommandQueue { get; protected set; }
    }
}
