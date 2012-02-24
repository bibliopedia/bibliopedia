using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FluentsNeeds;
using NUnit.Framework;

namespace Data.Tests
{
    public abstract class TestBase<T>
    {
        private AutoDatabase<T> Database;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Database = AutoDatabase<T>.Create(
                //PersistenceConfigurer.FileBasedDb("Test.sdf"), true);
                PersistenceConfigurer.SqlExpress("localhost", "bibliopedia"), true);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Database.Dispose();
        }
    }
}
