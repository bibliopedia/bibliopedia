using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;

namespace Data.FluentsNeeds
{
    public class IdentifiableOverride : IAutoMappingOverride<StringEntity>
    {
        #region IAutoMappingOverride<IIdentifiable> Members

        public void Override(AutoMapping<StringEntity> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.Custom<IdentifiableIdGenerator>();
            mapping.Id(x => x.Id).UnsavedValue(null);
            mapping.Id(x => x.Id)
                .CustomType("String")
                .CustomSqlType("nvarchar(512)");
        }

        #endregion
    }

    //public class 
}

