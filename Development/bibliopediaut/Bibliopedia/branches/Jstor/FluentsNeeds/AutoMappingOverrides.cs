using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.MappingModel.Collections;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using Jstor.Domain;
using NHibernate.Linq;
using NHibernate.Type;
using NHibernate.Dialect;
using NHibernate;
using Data;
using Data.FluentsNeeds;

namespace Jstor.FluentsNeeds
{

    public class StringEntityOverride : IAutoMappingOverride<StringEntity>
    {
        public void Override(AutoMapping<StringEntity> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.Custom<IdentifiableIdGenerator>();
            mapping.Id(x => x.Id).UnsavedValue(null);
            mapping.Id(x => x.Id)
                .CustomType("String")
                .CustomSqlType("nvarchar(255)");
        }
    }

    public class DcValueOverride : IAutoMappingOverride<Domain.DcValue>
    {
        public void Override(AutoMapping<Domain.DcValue> mapping)
        {
            mapping.SelectBeforeUpdate();
        }
    }

    public class DcOverride : IAutoMappingOverride<Domain.dc>
    {

        #region IAutoMappingOverride<dc> Members

        public void Override(AutoMapping<dc> mapping)
        {
            mapping.SelectBeforeUpdate();
            mapping.Map(x => x.Doi).Unique().Index("dc_doi")
                .CustomType("String")
                .CustomSqlType("nvarchar(255)");
            mapping.HasManyToMany(x => x.creator).AsSet();
            mapping.HasManyToMany(x => x.identifier).AsSet();
            mapping.HasManyToMany(x => x.type).AsSet();
            mapping.HasManyToMany(x => x.date).AsSet();
            mapping.HasManyToMany(x => x.subject).AsSet();
            mapping.HasManyToMany(x => x.ReferencedBy).AsSet();
            mapping.HasManyToMany(x => x.ReferenceIndexes).AsSet();
        }

        #endregion
    }

}
