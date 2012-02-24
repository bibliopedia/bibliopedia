//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using FluentNHibernate.Automapping.Alterations;
//using FluentNHibernate.Automapping;
//using WikiInteraction.Domain;
//using FluentNHibernate.Data;

//namespace WikiInteraction.FluentsNeeds
//{
//    public class JStorDoiAutoMappingOverrides : IAutoMappingOverride<JstorDoi>
//    {
//        public void Override(AutoMapping<Domain.JstorDoi> mapping)
//        {
//            mapping.ImportType<Domain.JstorDoi>();
//            mapping.Map(x => x.Doi)
//                .CustomType("String")
//                .CustomSqlType("nvarchar(128)");
//        }
//    }

//    public class EntityQueueAutoMappingOverride<T> : IAutoMappingOverride<EntityQueue<T>> where T : Entity
//    {
//        public void Override(AutoMapping<EntityQueue<T>> mapping)
//        {
//            mapping.ImportType<EntityQueue<T>>();
//            mapping.Map(x => x.QueueType).Index("idx_entityqueue_type").Not.Nullable().Unique()
//                .CustomType("String")
//                .CustomSqlType("nvarchar(2048)");
//        }
//    }
//}
