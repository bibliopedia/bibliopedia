using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Data;
using NHibernate;
using WikiInteraction.Domain;
using Data.FluentsNeeds;
using Jstor.Domain;
using NHibernate.Linq;

namespace WikiInteraction
{
    [TestFixture]
    public class EntityQueueTests
    {
        IAutoDatabase Database;
        ISession Session;

        public virtual QueueType GetQueue<QueueType>() where QueueType : EntityQueue { return EntityQueue.Get<QueueType>(Session); }

        [SetUp]
        public void SetUp()
        {
            Session = Database.SessionFactory.OpenSession();
        }

        [TearDown]
        public void TearDown()
        {
            if (Session == null) return;
            Session.Close();
            Session.Dispose();
        }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Database =
                AutoDatabase<JstorDoi>.Create(
                PersistenceConfigurer.FileBasedTempDb("JSTOR2"), true);
            //PersistenceConfigurer.SqlExpress("localhost", "JSTOR"), true);
        }

        [Test]
        public void CanCreateEntityQueue()
        {
            var q = EntityQueue.Create<CitationQueue>();
            Assert.NotNull(q);
        }

        [Test]
        public void CanLocatePersistentQueueByType()
        {
            var queue = GetQueue<TestQueue>();
            Assert.IsNotNull(queue);
        }

        [Test]
        public void CanPushItem()
        {
            int beforeCount, afterCount;
            using (var q1 = GetQueue<TestQueue>())
            {
                beforeCount = q1.Items.Count;
                q1.Push(new JstorDoi { Doi = @"10.2307/1618158" });
                Assert.Greater(q1.Items.Count, 0);
            }

            using (var q2 = GetQueue<TestQueue>())
            {
                afterCount = q2.Items.Count;
                Assert.AreEqual(beforeCount+1, afterCount, "Counts are wrong");
            }            
        }

        [Test]
        public void CanPopItem()
        {
            var item = new JstorDoi { Doi = @"10.2307/1618158" };

            int beforeCount, afterCount;
            using (var q1 = GetQueue<TestQueue>())
            {
                beforeCount = q1.Items.Count;
                q1.Push(item);
                Assert.Greater(q1.Items.Count, 0);
            }

            using (var q2 = GetQueue<TestQueue>())
            {
                afterCount = q2.Items.Count;
                Assert.AreEqual(beforeCount + 1, afterCount, "Counts are wrong after push");
                var item2 = q2.Pop();
                Assert.AreEqual(item.Doi, item2.Doi);
            }

            using (var q3 = GetQueue<TestQueue>())
            {
                Assert.AreEqual(beforeCount, q3.Items.Count, "Counts are wrong after pop");
            }
        }
    }
}
