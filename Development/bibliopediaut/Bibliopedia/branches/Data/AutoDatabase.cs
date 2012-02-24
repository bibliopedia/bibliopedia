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
using Jstor.FluentsNeeds;
using NHibernate.Event;
using NHibernate.Event.Default;

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

        // FluentMonad
        // Save the space of X = X.Operation1, x=x.Operation2, etc.
        // But preserve debugability (and the ability to makde function calls 
        // etc within the workflow to fetch data)

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
                                var x = AutoMap.Assembly(AssemblyBeingMapped);
                                x = x.Alterations(
                                    coll => 
                                        {
                                            coll.Add(
                                                new AutoDetectStrategies(
                                                    AssemblyBeingMapped,
                                                    Conventions.DomainTypeFilter));
                                        });
                                x = x.Where(Conventions.DomainTypeFilter);
                                x = x.Conventions.Add<Conventions>();
                                x = x.Conventions.Add<CustomForeignKeyConvention>();
                                x = x.Conventions.Add<CustomManyToManyTableNameConvention>();
                                x = x.Override<StringEntity>((mapping) => new IdentifiableOverride().Override(mapping));
                                x = x.UseOverridesFromAssemblyOf<T>();
                                m.AutoMappings.Add(x);
                                m.MergeMappings();

                            })
                        .ExposeConfiguration(Configure)
                        .BuildSessionFactory();
                return SessionFactoryInstance;
            }
        }

        protected virtual void Configure(Configuration configuration)
        {
            configuration.EventListeners.SaveEventListeners =
                new[] { new IdentifiableEventListeners(new DefaultSaveEventListener()) };
            configuration.EventListeners.SaveOrUpdateEventListeners =
                new[] { new IdentifiableEventListeners(new DefaultSaveOrUpdateEventListener()) };
            configuration.EventListeners.UpdateEventListeners =
                new[] { new IdentifiableEventListeners(new DefaultUpdateEventListener()) };


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


