using Data.Tests;
using FluentNHibernate.Testing;
using NUnit.Framework;
using PublishedWorks.Domain;

namespace PublishedWorks.Tests
{
    [TestFixture]
    public class MappingsAreWorking : TestBase<PersistedObject>
    {

        [Test]
        public void Can_Correctly_Map_Author()
        {
            new PersistenceSpecification<Author>(Database.Session)
                .CheckProperty(c => c.Name, "Sir John Mandeville")
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_Subject()
        {
            new PersistenceSpecification<Subject>(Database.Session)
                .CheckProperty(c => c.Value, "Travel")
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_WorkDetails()
        {
            new PersistenceSpecification<WorkDetails>(Database.Session)
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_LibraryIdentifier()
        {
            new PersistenceSpecification<LibraryIdentifier>(Database.Session)
                .CheckProperty(c => c.Type, LibraryIdType.Isbn)
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_MinedData()
        {
            new PersistenceSpecification<MinedData>(Database.Session)
                .CheckProperty(c => c.DataSource, DataSourceType.LibraryOfCongress)
                .VerifyTheMappings();            
        }

        [Test]
        public void Can_Correctly_Map_Work()
        {
            new PersistenceSpecification<Work>(Database.Session)
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_DataMinedWork()
        {
            new PersistenceSpecification<DataMinedWork>(Database.Session)
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_Publisher()
        {
            new PersistenceSpecification<Publisher>(Database.Session)
                .CheckProperty(p => p.Location, "France")
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_Book()
        {
            new PersistenceSpecification<Book>(Database.Session)
                .CheckProperty(b => b.PublishDate, "1/1/1357")
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_Article()
        {
            new PersistenceSpecification<Article>(Database.Session)
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_Range()
        {
            new PersistenceSpecification<Range>(Database.Session)
                .CheckProperty(r => r.First, 0)
                .CheckProperty(r => r.Last, 100)
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_Journal()
        {
            new PersistenceSpecification<Journal>(Database.Session)
                .CheckProperty(j => j.Title, "Journal of Learned Writings")
                .CheckProperty(j => j.Edition, "January 2000")
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_BinaryData()
        {
            var array = new byte[0];
            new PersistenceSpecification<BinaryData>(Database.Session)
                .CheckList(d => d.Bits, array)
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_JsonData()
        {
            new PersistenceSpecification<JsonData>(Database.Session)
                .CheckProperty(d => d.Json, "[]")
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_TextData()
        {
            new PersistenceSpecification<TextData>(Database.Session)
                .CheckProperty(d => d.Text, "The quick brown fox jumped over the lazy dog.")
                .VerifyTheMappings();
        }

        [Test]
        public void Can_Correctly_Map_XmlData()
        {
            new PersistenceSpecification<XmlData>(Database.Session)
                .CheckProperty(d => d.Xml, "")
                .VerifyTheMappings();
        }
    }
}


