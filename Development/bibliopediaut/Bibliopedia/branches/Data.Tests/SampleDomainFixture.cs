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
                    PersistenceConfigurer.FileBasedTempDb("d:/Test"), true);

            using (var session = database.SessionFactory.OpenSession())
            {
                var concrete0 = new Concrete { Value = "A" };
                var concrete1 = new Concrete { Value = "B" };
                var referrer = new Referrer();
                referrer.Items.Add(concrete0);
                referrer.Items.Add(concrete1);

                using (var trans = session.BeginTransaction())
                {
                    session.Save(referrer);
                    trans.Commit();
                }

            }
        }
    }
}
