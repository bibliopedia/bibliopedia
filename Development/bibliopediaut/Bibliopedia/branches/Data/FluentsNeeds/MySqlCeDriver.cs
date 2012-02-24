using System.Data;
using System.Data.SqlServerCe;
using NHibernate.Driver;
using NHibernate.SqlTypes;

namespace Data.FluentsNeeds
{
    /// <summary>
    /// Overridden Nhibernate SQL CE Driver,
    /// so that ntext fields are not truncated at 4000 characters
    /// </summary>
    public class MySqlCeDriver : SqlServerCeDriver
    {
        protected override void InitializeParameter(
            IDbDataParameter dbParam,
            string name,
            SqlType sqlType)
        {
            base.InitializeParameter(dbParam, name, sqlType);

            if (sqlType is StringClobSqlType)
            {
                var parameter = (SqlCeParameter)dbParam;
                parameter.SqlDbType = SqlDbType.NText;
            }
        }
    }
}
