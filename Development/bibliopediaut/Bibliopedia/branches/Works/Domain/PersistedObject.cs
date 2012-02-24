// ReSharper disable PublicConstructorInAbstractClass

using System;
using FluentNHibernate.Data;

namespace PublishedWorks.Domain
{
    public abstract class PersistedObject : Entity
    {
        public PersistedObject()
        {
            CreatedAt = DateTime.Now;    
        }

        public virtual DateTime CreatedAt { get; protected set; }
    }
}


