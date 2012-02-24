using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.MappingModel.Collections;

namespace Jstor.FluentsNeeds
{
    //public class IdentifierOverride : IAutoMappingOverride<Domain.identifier>
    //{
    //    public void Override(AutoMapping<Domain.identifier> mapping)
    //    {
    //        mapping.ImportType<Domain.identifier>();
    //        mapping.Map(x => x.Value).Index("idx_identifiervalue").Unique().Not.Nullable()
    //            .CustomType("String")
    //            .CustomSqlType("nvarchar(4000)").Update();
    //    }
    //}

    //public class CreatorOverride : IAutoMappingOverride<Domain.creator>
    //{
    //    public void Override(AutoMapping<Domain.creator> mapping)
    //    {
    //        mapping.ImportType<Domain.creator>();
    //        mapping.Map(x => x.Value).Index("idx__creatorvalue").Unique().Not.Nullable()
    //            .CustomType("String")
    //            .CustomSqlType("nvarchar(4000)").Update();
    //    }
    //}

    public class DcOverride : IAutoMappingOverride<Domain.dc>
    {
        public void Override(AutoMapping<Domain.dc> mapping)
        {
            mapping.ImportType<Domain.dc>();
            mapping.Map(x => x.Sha1Hash).Index("idx_dc_sha1hash").Not.Nullable().Unique()
                .CustomType("String")
                .CustomSqlType("nvarchar(4000)");
        }
    }
}
