using System;
using System.Collections.Generic;
using System.Text;
using Data;
using FluentNHibernate.Data;
using Iesi.Collections.Generic;
using Jstor.Domain;

namespace JstorCrawl.Domain
{
    public class CommandResult : Entity
    {
        public CommandResult() 
        {
            Dois = new HashedSet<RecordValue>();
            Subjects = new HashedSet<RecordValue>();
            Creators = new Iesi.Collections.Generic.SortedSet<RecordValue>();
        }

        public CommandResult(string summary)
            : this()
        {
            Summary = summary;
        }

        public static CommandResult operator +(CommandResult lhs, CommandResult rhs)
        {
            var result = new CommandResult(lhs.Summary + "+" + rhs.Summary);
            result.Dois.AddAll(lhs.Dois.Union(rhs.Dois));
            result.Subjects.AddAll(lhs.Subjects.Union(rhs.Subjects));
            result.Creators.AddAll(lhs.Creators.Union(rhs.Creators));

            return result;
        }

        public static CommandResult operator +(CommandResult lhs, dc rhs)
        {
            var result = new CommandResult("");
            result.Dois.Add(rhs.Doi);
            result.Subjects.AddAll(lhs.Subjects.Union(rhs.Subjects));
            result.Creators.AddAll(lhs.Creators.Union(rhs.Creators));

            return result;
        }

        public virtual string Summary { get; protected set; }
        public Iesi.Collections.Generic.ISet<RecordValue> Dois { get; protected set; }
        public Iesi.Collections.Generic.ISet<RecordValue> Subjects{ get; protected set; }
        public Iesi.Collections.Generic.ISet<RecordValue> Creators { get; protected set; }

        public void SetSummary(string summary)
        {
            Summary = summary;
        }
    }
}