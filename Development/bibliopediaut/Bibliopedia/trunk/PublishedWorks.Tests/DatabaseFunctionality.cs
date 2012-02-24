using Data.Tests;
using NUnit.Framework;
using PublishedWorks.Domain;

namespace PublishedWorks.Tests
{
    [TestFixture]
    public class DatabaseFunctionality : TestBase<PersistedObject>
    {
        private static Work _bookA = null;
        static Work BookA 
        {
            get
            {
                if (_bookA == null)
                {
                    _bookA = new Work { Title = "This title" };
                    _bookA.Details.Authors.Add(new Author { Name = "Joe" });
                    Repositories.Work.Save(_bookA);
                }
                return _bookA;
            }
        }

        static Work BookB
        {
            get
            {
                var w = new Work { Title = "That title" };
                w.Details.Authors.Add(new Author { Name = "Jim" });
                Repositories.Work.Save(w);
                return w;
            }
        }

        [Test]
        public void Can_Save_Work()
        {
            Assert.NotNull(BookA);
            Assert.Greater(BookA.Id, 0);
        }
        
        [Test]
        public void Can_CrossReference_Two_Works()
        {
            var id = BookA.Id;
            BookA.Details.CrossReferences.Add(BookB);
            Repositories.Work.Save(BookA);
            Assert.AreEqual(id, BookA.Id); // Should not have changed
        }

        // Can_Save_Article_In_Journal
        // Can_Save_Book
        // Can_Save_Article
    }
}


