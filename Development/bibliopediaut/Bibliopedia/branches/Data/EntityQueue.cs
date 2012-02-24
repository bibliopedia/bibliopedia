using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Linq;
using Data;
using Data.Extensions;

namespace Data
{
    public class QueueItem<T> : Entity where T : IIdentifiable
    {
        public QueueItem()
        {
        }

        public QueueItem(string queueType, T item)
        {
            QueueType = queueType;
            Item = item;
        }

        public virtual string QueueType { get; protected set; }
        public virtual T Item { get; protected set; }
    }


    public class EntityQueue<TEntity> : PersistSelfOnDispose
    {
        public EntityQueue() { }

        public EntityQueue(ISession session)
            : base(session)
        {
        }

        public virtual string QueueType { get; protected set; }

        public IQueryable<TEntity> FetchItems()
        {
            return GetSession().TransactedOperation<IQueryable<TEntity>>(
                (session) =>
                    {
                        var qitems = from item in session.Linq<EntityQueue<TEntity>>()
                                     where item.QueueType == QueueType
                                     select item;
                        return qitems;
                    },
                x => SessionExtensions.ExceptionRethrow.ReturnNull) as IQueryable<TEntity>;
        }

        public virtual int Count()
        {
            return (FetchItems() ?? (new TEntity[] {}).AsQueryable()).Count();
        }

        public static T Create<T>() where T : EntityQueue<TEntity>
        {
            return Create<T>(null);
        }

        /// <summary>
        /// Creates an entity.  Used by Get
        /// </summary>
        /// <returns>Hibernate's copy of a new queue</returns>
        private static T Create<T>(ISession session) where T : EntityQueue<TEntity>
        {
            var item = Activator.CreateInstance<T>();
            item.SetSession(session);
            item.QueueType = typeof(T).AssemblyQualifiedName;
            
            session.TransactedSave(item);            

            // Always want to deal with the hibernate copy of the object
            return session.Get<T>(item.Id);
        }

        /// <summary>
        /// Gets a queue by type.  If it does not exist, queue is created
        /// </summary>
        /// <typeparam name="QueueType">Type of items stored in the queue</typeparam>
        /// <param name="session">Session to be used for queue operations</param>
        /// <returns>Hibernate's copy of a persistent queue that stores type TEntity</returns>
        public static QueueType Get<QueueType>(ISession session) where QueueType : EntityQueue<TEntity>
        {
            var res = from item in session.Linq<QueueType>()
                      where item.QueueType == typeof(QueueType).ToString()
                      select item;
            if (res.FirstOrDefault() == null)
            {
                session.TransactedSave(EntityQueue<TEntity>.Create<QueueType>(session));
                return Get<QueueType>(session); // return Hibernate's copy
            }
            
            var result = res.First<QueueType>();
            result.SetSession(session);
            return result;
        }

        public virtual void Merge(EntityQueue<TEntity> other)
        {
            TEntity item;
            while((item = other.Pop()) != null)
            {
                Push(item);
            }
        }

        /// <summary>
        /// Push an item into the queue (database table)
        /// </summary>
        public virtual void Push(TEntity item)
        {
            GetSession().TransactedSave(item);
        }

        /// <summary>
        /// Pop an item from the database table
        /// </summary>
        /// <returns>The item or null if the queue is empty</returns>
        public virtual TEntity Pop()
        {
            TEntity result = default(TEntity);
            bool success = false;
            while (!success)
            {
                try
                {
                    result = (from item in GetSession().Linq<TEntity>()
                              select item).FirstOrDefault();
                    GetSession().TransactedDelete(result);
                    success = true;
                }
                catch
                {
                    success = false;
                }
            }
            return result;
        }

    }
}
