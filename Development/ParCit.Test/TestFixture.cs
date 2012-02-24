using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Bibliopedia.ParsCit;
using NUnit.Framework;

namespace ParCit.Test
{
    [TestFixture]
    public class ParsCitServiceIntegrationTests
    {
        [Test]
        public void is_service_running()
        {
            Service.

            Assert.IsNotNullOrEmpty(result);
        }

        [Test]
        public void is_result_deserializable_as_json()
        {
            var service = new Bibliopedia.ParsCit.ParsCitService();

            string citeFile;
            string bodyFile;
            var result = service.extractCitations("FleckMandeville.txt", "jstor", out citeFile, out bodyFile);

            Assert.IsNotNull(result);
            var s = new System.Xml.Serialization.XmlSerializer(typeof(algorithm));
            var deserialized = (algorithm)s.Deserialize(new StringReader(result));

            Assert.IsNotNull(deserialized);
        }

        //// This test fail for example, replace result or delete this test to see all tests pass
        //[Test]
        //public void CanFtp()
        //{
        //}
    }
}
