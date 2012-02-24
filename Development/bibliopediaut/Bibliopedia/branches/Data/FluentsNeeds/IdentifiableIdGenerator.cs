using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.FluentsNeeds
{
    public class IdentifiableIdGenerator : NHibernate.Id.IIdentifierGenerator
    {
        #region IIdentifierGenerator Members

        // this piece of code was difficult for me to come up with.
        public object Generate(NHibernate.Engine.ISessionImplementor session, object obj)
        {
            var record = obj as IIdentifiable;
            if (record == null) throw new ArgumentOutOfRangeException("obj");

            return record.ComputeIdentity();
        }

        #endregion
    }
}
