using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SemWeb;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Jstor.JstorService;
using NC3A.SI.Rowlex;

namespace PersistJstorAsTriples
{
    [TestFixture]
    public class DublinCoreConverterTests
    {
        public IEnumerable<Jstor.Domain.dc> EnumerateCannedInput()
        {
            XmlReader reader;
            XmlSerializer serializer;
            searchRetrieveResponse response;
            using (var fileStream = new FileStream("Mandeville.xml", FileMode.Open))
            using (reader = XmlReader.Create(fileStream))
            {
                serializer = new XmlSerializer(typeof(searchRetrieveResponse));
                response = serializer.Deserialize(reader) as searchRetrieveResponse;
            }

            foreach (var record in response.records)
            {
                foreach (var data in record.recordData)
                {
                    yield return new Jstor.Domain.dc(data);
                }
            }   
        }

        [Test]
        public void CanConvertCannedDcsToRdf()
        {
            var store = new TripleStore();
            var converter = new DublinCoreConverter(store);

            converter.Convert(EnumerateCannedInput());

            converter.RdfDocument.ExportToN3("testoutputn3.rdf");
            converter.RdfDocument.ExportToRdfXml("testoutputXML.rdf");
        }
    }
}
