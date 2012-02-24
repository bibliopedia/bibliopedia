using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor.Domain;
using Data;
using Data.FluentsNeeds;
using Data.Extensions;
using System.IO;
using NHibernate;
using NHibernate.Linq;
using Jstor.Data;

namespace Jstor
{
    public interface IRepository
    {
        void Save(dc item);
        void Delete(dc item);
        dc Get(long id);
        INHibernateQueryable<dc> Linq();
    }

    public class Repository : IRepository
    {
        private const string DbFileName = "d:/JSTOR_DC_REPO";
        private static IAutoDatabase Database = CreateDatabase();
        private static IAutoDatabase CreateDatabase()
        {
            bool buildSchema = !File.Exists(DbFileName+".sdf");
            return
                JstorDatabase.Create(
                PersistenceConfigurer.PersistentFileBasedDb(DbFileName), buildSchema);
        }

        private readonly object SyncRoot = new object();
        private readonly ISession _session;

        public Repository()
        {
            _session = Database.SessionFactory.OpenSession();
        }

        #region IRepository Members

        public void Save(dc item)
        {
            lock (SyncRoot)
            {
                var id = item.ComputeIdentity() as string;
                var result = (from record in _session.Linq<dc>()
                              where record.Id == id
                              select record).FirstOrDefault();

                if (result == null) 
                    _session.TransactedSave(item);
                else
                {
                    result.MergeWith(item);
                    _session.TransactedUpdate(result);
                }
            }
        }

        public void Delete(dc item)
        {
            lock (SyncRoot) _session.TransactedDelete(item);
        }

        public dc Get(long id)
        {
            lock (SyncRoot) return _session.Get<dc>(id);
        }

        public INHibernateQueryable<dc> Linq()
        {
            lock (SyncRoot) return _session.Linq<dc>();
        }

        #endregion


        public dc GetByDoi(string doi)
        {
            var result = (from record in Linq()
                          where record.Doi == doi
                          select record).FirstOrDefault();
            return result;
        }
    }
}
