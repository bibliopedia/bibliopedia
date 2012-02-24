using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Jstor.FluentsNeeds;
using NHibernate.Event;
using System.Reflection;
using FluentNHibernate.Cfg.Db;

namespace Jstor.Data
{
    public class JstorDatabase : AutoDatabase<Jstor.Domain.dc>
    {
        public JstorDatabase(
            Assembly assemblyBeingMapped,
            IPersistenceConfigurer persistenceConfigurer,
            bool buildSchema)
            : base(assemblyBeingMapped, persistenceConfigurer, buildSchema)
        {
        }

        public static new JstorDatabase Create(
            IPersistenceConfigurer persistenceConfigurer,
            bool buildSchema)
        {
            return new JstorDatabase(typeof(Jstor.Domain.dc).Assembly, persistenceConfigurer, buildSchema);
        }


        protected override void Configure(NHibernate.Cfg.Configuration configuration)
        {
            base.Configure(configuration);
        }
    }
}
