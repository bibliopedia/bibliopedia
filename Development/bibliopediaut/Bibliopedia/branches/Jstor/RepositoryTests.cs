using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Jstor.Domain;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Jstor
{
    [TestFixture]
    public class RepositoryTests
    {
        private Repository Repository;

        [SetUp]
        public void SetUp()
        {
            Repository = new Repository();
        }

        [Test]
        public void Dc1AndDc2HaveSameId()
        {
            Assert.AreEqual(dc.TestDc1().Id, dc.TestDc2().Id);
        }

        [Test]
        public void Dc1AndDc2HaveNonNullIDs()
        {
            Assert.NotNull(dc.TestDc1().ComputeIdentity(), "dc1");
            Assert.NotNull(dc.TestDc2().ComputeIdentity(), "dc2");
        }

        [Test]
        public void TwoDcsWithSameHashWillGetMerged()
        {
            Repository.Save(dc.TestDc1());
            Repository.Save(dc.TestDc2());

            var item = (from record in Repository.Linq()
                        where record == dc.TestDc1()
                        select record).First();

            Assert.AreEqual(dc.TestDc1().coverage, item.coverage, "Coverage is wrong");
            Assert.AreEqual(dc.TestDc2().description, item.description, "Description is wrong");
        }


        private static SearchResponseCallbacks GetCannedData()
        {
            var callbacks = new SearchResponseCallbacks();
            var processor = new Restful.ParallelProcessor(callbacks);
            var file = new FileStream(@"Mandeville.xml", FileMode.Open);
            var reader = XmlReader.Create(file);
            processor.ParallelProcessXml(reader);
            var response = callbacks;
            //processor.Process(@"http://dfr.jstor.org/sru/?operation=searchRetrieve&query=dc.subject+%3D+mandeville&version=1.1");
            //processor.Process(@"http://dfr.jstor.org/sru/?operation=searchRetrieve&recordPacking=xml&version=1.1&startRecord=1&maximumRecords=784&resultSetTTL=&recordSchema=info:srw/schema/1/dc-v1.1&query=dc.subject%20=%20mandeville");
            return callbacks;
        }

        [Test]
        public void CanPersistCannedData()
        {
            foreach (var item in dc.FromResponse(GetCannedData().Response))
                Repository.Save(item);
        }

        [Test]
        public void TwoDcsHaveMoreThanOneCreator()
        {
            Assert.Greater(dc.TestDc1().creator.Count, 1, "dc1");
            Assert.Greater(dc.TestDc2().creator.Count, 1, "dc2");
        }

        [Test]
        public void CanDownloadRealSearch()
        {
            var search = new Search();
            var results = search.SearchAnywhere("mandeville's travels");
            var count = 0;
            foreach (var result in results)
            {
                Repository.Save(result);
                Debug.WriteLine("#" + count + ": " + result.title);
                count++;
            }
            Assert.IsTrue(count > 200, "Count too low");
            count++;
        }



        
        /// <summary>
        /// Establish a cross-reference between two works
        /// </summary>
        /// <param name="record"></param>
        /// <param name="doi"></param>
        /// <returns> <</returns>
        /// 
        public int CrossReference(dc record, string doi)
        {
            if (record.Doi != null)
            {
                record.ReferenceIndexes.Add(new DcValue(doi));

                var other = Repository.GetByDoi(record.Doi);
                if (other != null)
                {
                    other.ReferencedBy.Add(new DcValue(record.Doi));
                    return 2;
                }
                return 1;
            }
            return 0;
        }

        [Test]
        public void CanGetCitationsForPersistedItems()
        {
            var results = from item in Repository.Linq()
                           select item;
            Assert.IsNotNull(results.DefaultIfEmpty(null), "Must put some items in database before running this test");

            bool foundCitation = false;
            foreach (var result in results)
            {
                foreach (var citation in result.Citations())
                {
                    CrossReference(result, citation.Doi);
                }
                Repository.Save(result);

            }

            Assert.IsTrue(foundCitation, "Found no citations at all");
        }
    }
}
