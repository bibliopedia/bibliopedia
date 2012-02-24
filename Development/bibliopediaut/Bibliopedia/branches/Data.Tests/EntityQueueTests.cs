using NUnit.Framework;
using NHibernate;
using Data.FluentsNeeds;

namespace Data.Tests
{
    public class ValueClass : StringEntity
    {
        public ValueClass()
        {
        }

        public ValueClass(string value)
        {
            Value = value; 
        }

        public virtual string Value { get; protected set; }

        public override object ComputeIdentity()
        {
            return Value;
        }
    }

    public class QueueOne : EntityQueue<ValueClass> {}
    public class QueueTwo : EntityQueue<ValueClass> {}

    [TestFixture]
    public class EntityQueueTests
    {
        IAutoDatabase _database;
        ISession _session;

        public virtual TQueueType GetQueue<TQueueType>() where TQueueType : EntityQueue<ValueClass> { return EntityQueue<ValueClass>.Get<TQueueType>(_session); }



        [SetUp]
        public void SetUp()
        {
            _session = _database.SessionFactory.OpenSession();
        }

        [TearDown]
        public void TearDown()
        {
            if (_session == null) return;
            _session.Close();
            _session.Dispose();
        }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _database =
                AutoDatabase<ValueClass>.Create(
                    PersistenceConfigurer.FileBasedTempDb(typeof (EntityQueueTests).ToString()), true);
            //PersistenceConfigurer.SqlExpress("localhost", "JSTOR"), true);
        }

        [Test]
        public void CanCreateEntityQueue()
        {
            using (var q = EntityQueue<ValueClass>.Create<QueueOne>())
            {
                Assert.NotNull(q);
            }
        }

        [Test]
        public void CanLocatePersistentQueueByType()
        {
            using (var queue = GetQueue<QueueOne>())
            {
                Assert.IsNotNull(queue);
            }
        }

        [Test]
        public void CanPushItem()
        {
            int beforeCount, afterCount;
            using (var q1 = GetQueue<QueueOne>())
            {
                beforeCount = q1.Count();
                q1.Push(new ValueClass(@"10.2307/1618158"));
                Assert.Greater(q1.Count(), 0);
            }

            using (var q2 = GetQueue<QueueOne>())
            {
                afterCount = q2.Count();
                Assert.AreEqual(beforeCount+1, afterCount, "Counts are wrong");
            }            
        }

        [Test]
        public void CanPopItem()
        {
            var item = new ValueClass(@"10.2307/1618158");

            int beforeCount, afterCount;
            using (var q1 = GetQueue<QueueOne>())
            {
                beforeCount = q1.Count();
                q1.Push(item);
                Assert.Greater(q1.Count(), 0);
            }

            using (var q2 = GetQueue<QueueOne>())
            {
                afterCount = q2.Count();
                Assert.AreEqual(beforeCount + 1, afterCount, "Counts are wrong after push");
                var item2 = q2.Pop();
                Assert.AreEqual(item.Value, item2.Value);
            }

            using (var q3 = GetQueue<QueueOne>())
            {
                Assert.AreEqual(beforeCount, q3.Count(), "Counts are wrong after pop");
            }
        }

        [Test]
        public void PushedItemHasAnId()
        {
            var item = new ValueClass(@"10.2307/1618158");

            using (var q1 = GetQueue<QueueOne>())
            {
                q1.Push(item);
                Assert.Greater(item.Id, 0);
            }
        }
    }
}
