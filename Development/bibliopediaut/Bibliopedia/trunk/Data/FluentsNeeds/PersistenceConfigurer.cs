using System;
using FluentNHibernate.Cfg.Db;
using System.Data.SqlServerCe;
using System.IO;

namespace Data.FluentsNeeds
{
    public class PersistenceConfigurer
    {
        public static IPersistenceConfigurer SqlExpress(string machineName, string databaseName)
        {
            return
                MsSqlConfiguration.MsSql2008.ConnectionString(
                    String.Format(@"Data Source={0}\SQLEXPRESS;Initial Catalog={1};Integrated Security=True;Pooling=False", machineName, databaseName));
        }

        public static IPersistenceConfigurer FileBasedTempDb(string template)
        {
            var fileName = template + "-" + 
                DateTime.Now.ToString()
                    .Replace(":","-")
                    .Replace(" ", "_")
                    .Replace("/", "-") + ".sdf";

            var connString = String.Format(@"Data Source='{0}'", fileName);

            using (var engine = new SqlCeEngine())
            {
                engine.LocalConnectionString = connString;
                engine.CreateDatabase();
            }

            var configurer = MsSqlCeConfiguration.Standard.ConnectionString(connString);
            configurer.Driver("Data.FluentsNeeds.MySqlCeDriver, Data");
            return configurer;
        }

        public static IPersistenceConfigurer PersistentFileBasedDb(string fileName)
        {
            fileName += ".sdf";
            var connString = String.Format(@"Data Source='{0}'", fileName);

            if (!File.Exists(fileName))
            {
                using (var engine = new SqlCeEngine())
                {
                    engine.LocalConnectionString = connString;
                    engine.CreateDatabase();
                }
            }

            var configurer = MsSqlCeConfiguration.Standard.ConnectionString(connString);
            configurer.Driver("Data.FluentsNeeds.MySqlCeDriver, Data");
            return configurer;
        }
    }
}


