using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Data;

namespace Data
{
    public class PersistenceVerbs
    {
        public static object Save<T>(ISession session, T entity) where T : Entity
        {
            return PersistenceVerbs.TransactedOperation<T>(
                session, 
                x => x.Save(entity),
                x => ExceptionRethrow.Rethrow);
        }

        public static object Delete<T>(ISession session, T entity) where T : Entity
        {
            return PersistenceVerbs.TransactedOperation<T>(
                session,
                x => { x.Delete(entity); return 0; },
                x => ExceptionRethrow.Rethrow);
        }

        public static object Update<T>(ISession session, T entity) where T : Entity
        {
            return PersistenceVerbs.TransactedOperation<T>(
                session,
                x => { x.Update(entity); return 0; },
                x => ExceptionRethrow.Rethrow);
        }

        public enum ExceptionRethrow
        {
            Rethrow = 0,
            ReturnNull
        }

        public static object TransactedOperation<TEntity>(
            ISession session,
            Func<ISession, object> operation,
            Func<Exception, ExceptionRethrow> onError) where TEntity : Entity
        {
            using (var trans = session.BeginTransaction())
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
