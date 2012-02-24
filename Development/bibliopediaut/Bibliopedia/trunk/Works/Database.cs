using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Data.FluentsNeeds;
using NHibernate;
using PublishedWorks.Domain;

namespace PublishedWorks
{
    public static class Database
    {
        private static readonly IAutoDatabase Instance; 
        static Database()
        {
            Instance = AutoDatabase<PersistedObject>.Create(
                PersistenceConfigurer.SqlExpress(Environment.MachineName, "Bibliopedia"),
                //PersistenceConfigurer.FileBasedDb("Test.sdf"),
             true);

            Session = Instance.SessionFactory.OpenSession();
        }

        public static ISession Session { get; private set; }
    }
}
