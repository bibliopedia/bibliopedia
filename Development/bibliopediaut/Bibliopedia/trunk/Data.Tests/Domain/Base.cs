using System;
using FluentNHibernate.Data;

namespace Data.Tests.Domain
{
    public abstract class Base : Entity
    {
        protected Base() 
        {
            CreatedAt = DateTime.Now; 
        }
        public virtual DateTime CreatedAt { get; protected set; }
    }
}