//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using FluentNHibernate.Data;
//using NHibernate;
//using NHibernate.Linq;
//using Data;
//using Data.Extensions;

//namespace WikiInteraction.Domain
//{
//    public abstract class PersistSelfOnDispose : Entity, IDisposable
//    {

//        private ISession _session;

//        public PersistSelfOnDispose() { }
//        public PersistSelfOnDispose(ISession session) { _session = session; }

//        public virtual void Dispose()
//        {
//            Dispose(true);
//        }

//        public virtual ISession GetSession()
//        {
//            return _session;
//        }

//        public virtual void SetSession(ISession session)
//        {
//            _session = session;
//        }

//        private bool _disposed = false;
//        protected virtual void Dispose(bool disposing)
//        {
//            if (_disposed) return;
//            if (!_disposed)
//            {
//                if (_session != null) _session.TransactedSave(this);
//                _disposed = true;
//            }
//        }

//    }

//    public class EntityQueue<TEntity> : PersistSelfOnDispose where TEntity : Entity
//    {
//        public EntityQueue() { }

//        public EntityQueue(ISession session)
//            : base(session)
//        {
//        }

//        public virtual string QueueType { get; protected set; }

//        public virtual IList<TEntity> Items { get; protected set; }

//        public static T Create<T>() where T : EntityQueue<TEntity>
//        {
//            return Create<T>(null);
//        }

//        /// <summary>
//        /// Creates an entity.  Used by Get
//        /// </summary>
//        /// <returns>Hibernate's copy of a new queue</returns>
//        private static T Create<T>(ISession session) where T : EntityQueue<TEntity>
//        {
//            T item = Activator.CreateInstance<T>();
//            item.SetSession(session);
//            item.QueueType = typeof(T).AssemblyQualifiedName;
//            item.Items = new List<TEntity>();
            
//            session.TransactedSave(item);            

//            // Always want to deal with the hibernate copy of the object
//            return session.Get<T>(item.Id);
//        }

//        /// <summary>
//        /// Gets a queue by type.  If it does not exist, queue is created
//        /// </summary>
//        /// <typeparam name="QueueType">Type of items stored in the queue</typeparam>
//        /// <param name="session">Session to be used for queue operations</param>
//        /// <returns>Hibernate's copy of a persistent queue that stores type TEntity</returns>
//        public static QueueType Get<QueueType>(ISession session) where QueueType : EntityQueue<TEntity>
//        {
//            var res = from item in session.Linq<QueueType>()
//                      where item.QueueType == typeof(QueueType).ToString()
//                      select item;
//            if (res.FirstOrDefault() == null)
//            {
//                session.TransactedSave(EntityQueue<TEntity>.Create<QueueType>(session));
//                return Get<QueueType>(session); // return Hibernate's copy
//            }
            
//            var result = res.First<QueueType>();
//            result.SetSession(session);
//            return result;
//        }

//        /// <summary>
//        /// Push an item into the queue (database table)
//        /// </summary>
//        public virtual void Push(TEntity item)
//        {
//            GetSession().TransactedSave(item);
//        }

//        /// <summary>
//        /// Pop an item from the database table
//        /// </summary>
//        /// <returns>The item or null if the queue is empty</returns>
//        public virtual TEntity Pop()
//        {
//            TEntity result = null;
//            GetSession().Lock(Items, LockMode.Write);
//            lock (GetSession())
//            {
//                result = (from item in GetSession().Linq<TEntity>()
//                            select item).FirstOrDefault();
//                if (result != null) GetSession().TransactedDelete(result);
//            }
//            return result;
//        }
//    }

//}
