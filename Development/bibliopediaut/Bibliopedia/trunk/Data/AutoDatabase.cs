using System;
using System.Linq;
using System.Reflection;
using Data.FluentsNeeds;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Data
{
    public class AutoDatabase<T> : IDisposable, Data.IAutoDatabase
    {
        public static AutoDatabase<T> Create(
            IPersistenceConfigurer persistenceConfigurer,
            bool buildSchema)
        {
            return new AutoDatabase<T>(typeof(T).Assembly, persistenceConfigurer, buildSchema);
        }

        public AutoDatabase(
            Assembly assemblyBeingMapped, 
            IPersistenceConfigurer persistenceConfigurer)
            : this(assemblyBeingMapped, persistenceConfigurer, false)
        {
        }

        public AutoDatabase(
            Assembly assemblyBeingMapped, 
            IPersistenceConfigurer persistenceConfigurer, 
            bool buildSchema)
        {
            AssemblyBeingMapped = assemblyBeingMapped;
            PersistenceConfigurer = persistenceConfigurer;
            BuildSchema = buildSchema;
        }

        public Assembly AssemblyBeingMapped { get; private set; }
        public IPersistenceConfigurer PersistenceConfigurer { get; private set; }
        public bool BuildSchema { get; private set; }

        private ISessionFactory SessionFactoryInstance;

        public ISessionFactory SessionFactory
        {
            get
            {
                if (SessionFactoryInstance != null)
                {
                    return SessionFactoryInstance;
                }

                if (PersistenceConfigurer == null)
                {
                    throw new ApplicationException(
                        "PeristenceConfigurer must be set.  Canned examples in Bibliopedia.Data.PersistenceConfigurer");
                }

                SessionFactoryInstance =
                    Fluently.Configure()
                        .Database(PersistenceConfigurer)
                        .Mappings(m =>
                            {
                                var autoPersistenceModel =
                                    AutoMap.Assembly(AssemblyBeingMapped)
                                             .Alterations(
                                                  coll => coll.Add(
                                                      new AutoDetectStrategies(
                                                          AssemblyBeingMapped,
                                                          DomainTypeFilter)))
                                             .Where(DomainTypeFilter)
                                             .Conventions.Add<Conventions>()
                                             .UseOverridesFromAssemblyOf<T>();
                                m.AutoMappings.Add(autoPersistenceModel);
                                m.MergeMappings();
                            })
                        .ExposeConfiguration(Configure)
                        .BuildSessionFactory();
                return SessionFactoryInstance;
            }
        }

        public static bool DomainTypeFilter(Type type)
        {
            return type.Namespace.EndsWith("Domain");
        }

        private void Configure(Configuration configuration)
        {
            if (!BuildSchema) return;

            new SchemaExport(configuration).Drop(false, true);
            new SchemaExport(configuration).Create(true, true);
        }

        public void Dispose()
        {
            //SessionFactoryInstance.Dispose();
        }
    }
}


