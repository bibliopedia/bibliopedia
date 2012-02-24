using System;
using System.Linq.Expressions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Data;

namespace Data.FluentsNeeds
{
    public class Conventions :
        IHasOneConvention, 
        IHasManyConvention, 
        IReferenceConvention, 
        IHasManyToManyConvention,
        IPropertyConvention
    {
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
            if (instance.Property.ToString().Contains("String"))
            {
                instance.CustomType("StringClob");
                instance.CustomSqlType("NTEXT");
                //instance.Length(4001);
            }
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.AllDeleteOrphan();
        }
    }
}


