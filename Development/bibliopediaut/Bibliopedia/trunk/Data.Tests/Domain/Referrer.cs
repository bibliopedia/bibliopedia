using System;
using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Data.Tests.Domain
{
    public class Referrer : Entity
    {
        public Referrer()
        {
            Items = new List<Base>();
        }
        public virtual IList<Base> Items { get; protected set; }
        
    }
}


