using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using Jstor.Domain;

namespace Jstor
{
    [TestFixture]
    public class ResourceTests
    {
        // TODO: Make abstract
        public string LoadResource()
        {
            var resource = new Resource("468306");
            var xml = resource.Xml;
            Assert.IsNotNullOrEmpty(xml);
            return xml;
        }

        [Test]
        public void CanLoadResource()
        {
            LoadResource();
        }

        [Test]
        public void ArgumentContainsMultipleResults()
        {
            var doc = new XPathDocument("468306.xml");

            var results = 
                doc.SelectMultipleResults("/article/front/article-meta/contrib-group/contrib[@contrib-type='author']/name/surname");

            Assert.AreEqual(results.Count(), 1);
        }

        [Test]
        public void ArgumentContainsSingleResult()
        {
            var doc = new XPathDocument("468306.xml");

            var result =
                doc.Evaluate("string(/article[1]/front[1]/article-meta[1]/contrib-group[1]/contrib[1]/name[1]/surname[1])");

            Assert.IsTrue(result is string, "Type was: " + result.GetType().ToString());

        }

        [Test]
        public void CannedDocumentHasCitations()
        {
            var citations = (new Resource("468306")).Citations().ToList();
            Assert.Greater(citations.Count, 0, "Expected more citations.");
        }

    }
}
