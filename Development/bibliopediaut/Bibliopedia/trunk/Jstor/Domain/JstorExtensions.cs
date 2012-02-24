using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Jstor.Domain
{
    public static class JstorExtensions
    {
        public static string RawText(this JstorService.searchRetrieveResponseRecordsRecordRecordData recordData)
        {
            var serializer = new XmlSerializer(typeof(JstorService.searchRetrieveResponseRecordsRecordRecordData));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, recordData);
                return writer.GetStringBuilder().ToString();
            }   
        }

        public static IEnumerable<Resource.Citation> Citations(this Jstor.Domain.dc record)
        {
            var citations = new List<string>();

            if (record.identifier == null) yield break;

            foreach (var identifier in record.identifier)
            {
                var id = identifier.Value.Split(' ');
                if (id[0] == "UID:")
                {
                    var resource = new Resource(id[1]);
                    foreach (var citation in resource.Citations())
                    {
                        yield return citation;
                    }
                    yield break;
                }
            }

            
        }
    }
}
