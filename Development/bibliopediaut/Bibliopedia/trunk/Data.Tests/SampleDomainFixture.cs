using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FluentsNeeds;
using Data.Tests.Domain;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class SampleDomainFixture
    {

        [Test]
        public void CanSaveSampleDomainItem()
        {
            var database =
                AutoDatabase<Base>.Create(
                    PersistenceConfigurer.SqlExpress("localhost", "BibliopediaTest"), true);

            using (var session = database.SessionFactory.OpenSession())
            {
                var longString = new StringBuilder(16384);
                while (longString.Length < longString.Capacity) longString.Append("_");
                var concrete = new Concrete{Value = longString.ToString()};
                var referrer = new Referrer();
                referrer.Items.Add(concrete);

                using (var trans = session.BeginTransaction())
                {
                    session.Save(referrer);
                    trans.Commit();
                }

            }
        }
    }
}
