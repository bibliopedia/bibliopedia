using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Data;
using Jstor.Domain;
using Data;
using NHibernate;
using NHibernate.Linq;

namespace WikiInteraction.Domain
{
    public class JstorDoi : Entity
    {
        public virtual string Doi { get; set; } 
    }

    public abstract class PersistSelfOnDispose : Entity, IDisposable
    {

        private ISession _session;
       
        public PersistSelfOnDispose() { }
        public PersistSelfOnDispose(ISession session) { _session = session; }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        public virtual T WithSession<T>(ISession session) where T : EntityQueue
        {
            _session = session;
            return (T)this;
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (!_disposed)
            {
                if (_session != null) PersistenceVerbs.Save(_session, this);
                _disposed = true;
            }
        }

        public virtual ISession GetSession()
        {
            return _session;
        }
    }

    public class EntityQueue : PersistSelfOnDispose
    {
        public EntityQueue() { }

        public EntityQueue(ISession session) : base(session)
        {
        }

        public virtual string QueueType { get; protected set; }
        public virtual IList<JstorDoi> Items { get; protected set; }

        public static T Create<T>() where T : EntityQueue
        {
            return Create<T>(null);
        }

        public static T Create<T>(ISession session) where T : EntityQueue
        {
            T item = Activator.CreateInstance<T>();
            item.WithSession<T>(session);
            item.QueueType = typeof(T).ToString();
            item.Items = new List<JstorDoi>();
            return item;
        }

        public static QueueType Get<QueueType>(ISession session) where QueueType : EntityQueue
        {
            var res = from item in session.Linq<QueueType>()
                      where item.QueueType == typeof(QueueType).ToString()
                      select item;
            if (res.FirstOrDefault() == null)
            {
                PersistenceVerbs.Save(session, EntityQueue.Create<QueueType>(session));
                return Get<QueueType>(session); // return Hibernate's copy
            }

            var result = res.First<QueueType>().WithSession<QueueType>(session);
            return result;
        }

        public virtual void Push(JstorDoi item)
        {
            Items.Add(item);
        }

        public virtual JstorDoi Pop()
        {
            var item = Items[0];
            Items.RemoveAt(0);
            return item;
        }
    }

    public class TestQueue : EntityQueue { }
    public class ResultQueue : EntityQueue { }
    public class CitationQueue : EntityQueue { } 

    public class ForceDc : Jstor.Domain.dc { }
}
