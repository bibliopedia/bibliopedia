using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Data;

namespace Data.Extensions
{
    public static class SessionExtensions
    {
        public static object TransactedSave<T>(this ISession session, T entity)
        {
            return SessionExtensions.TransactedOperation<T>(
                session, 
                x => x.Save(entity),
                x => ExceptionRethrow.Rethrow);
        }

        public static object TransactedSave<T>(this ISession session, IEnumerable<T> entities)
        {
            return session.TransactedOperation<T>(
                x =>
                {
                    var count = 0;
                    foreach (var entity in entities) 
                    {
                        x.Save(entity);
                        count++; 
                    }
                    return count;
                },
                x => ExceptionRethrow.Rethrow);
        }

        public static void TransactedDelete<T>(this ISession session, T entity)
        {
            SessionExtensions.TransactedOperation<T>(
                session,
                x => { x.Delete(entity); return 0; },
                x => ExceptionRethrow.Rethrow);
        }

        public static void TransactedUpdate<T>(this ISession session, T entity)
        {
            SessionExtensions.TransactedOperation<T>(
                session,
                x => { x.Update(entity); return true; },
                x => ExceptionRethrow.ReturnNull);
        }

        public enum ExceptionRethrow
        {
            Rethrow = 0,
            ReturnNull
        }

        public static object TransactedOperation<TEntity>(
            this ISession session,
            Func<ISession, object> operation,
            Func<Exception, ExceptionRethrow> onError)
        {
            using (var trans = session.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = operation(session);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    var result = onError(ex);
                    trans.Rollback();
                    if (result == ExceptionRethrow.Rethrow) throw;
                    return null;
                }
            }
        }
    }
}
