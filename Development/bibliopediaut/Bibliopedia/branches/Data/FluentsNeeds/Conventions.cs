using System;
using System.Linq.Expressions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Data;
using System.Reflection;

namespace Data.FluentsNeeds
{
    public class Conventions :
        IHasOneConvention, 
        IHasManyConvention, 
        IReferenceConvention, 
        IHasManyToManyConvention,
        IPropertyConvention
    {
        public static Type AutoDetectStrategyBaseType() { return typeof(IEquatable<>); }

        public static bool DomainTypeFilter(Type type)
        {
            return type.Namespace.EndsWith("Domain");
        }

        public void Apply(IOneToOneInstance instance)
        {
            instance.Cascade.All();
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Cascade.All();
        }

        public void Apply(IPropertyInstance instance)
        {
            if (instance.Property.PropertyType.ToString().Contains("String"))
            {
                instance.CustomType("StringClob");
                instance.CustomSqlType("NTEXT");
                //instance.Length(4001);
            }
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.SaveUpdate();
        }
    }

    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(FluentNHibernate.Member property, Type type)
        {
            if (property == null)
                return type.Name + "ID";
            return property.Name + "ID";
        }
    }

    public class CustomManyToManyTableNameConvention : ManyToManyTableNameConvention
    {
        protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection,
            IManyToManyCollectionInspector otherSide)
        {
            return Inflector.Net.Inflector.Pluralize(collection.EntityType.Name) +
                Inflector.Net.Inflector.Pluralize(otherSide.EntityType.Name);
        }

        protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
        {
            return Inflector.Net.Inflector.Pluralize(collection.EntityType.Name) +
                Inflector.Net.Inflector.Pluralize(collection.ChildType.Name);
        }
    }


}


