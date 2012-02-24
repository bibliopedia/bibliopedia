using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace Jstor
{
    public class Resource
    {
        public class Citation
        {
            public string CitationOf { get; private set; }
            public string Identifier { get; private set; }
            public string Value { get; private set; }

            public Citation(string citationOf, string identifier, string value)
            {
                CitationOf = citationOf;
                Identifier = identifier;
                Value = value;
            }

            public override string ToString()
            {
                return CitationOf + "; " + Identifier
                    + "; " + Value;
            }
        }

        private static readonly NetworkCredential Creds = new NetworkCredential("mwidner", "biblio$Jstor0");
        public static readonly Uri JstorDfrUri = new Uri(@"http://dfr.jstor.org/resource");
        
        public Resource(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; private set; }

        public Uri Url { get { return new Uri(String.Format("{0}/{1}", JstorDfrUri.ToString(), Identifier)); } }

        private Uri CitationsUrl { get { return new Uri(String.Format("{0}?view=citations", Url)); } }

        private string GetXml(string url)
        {
            var stream = Restful.ParallelProcessor.InvokePoxWebRequestWithCredentials(url, Creds);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public string Xml
        {
            get
            {
                return GetXml(Url.ToString());
            }
        }

        public string CitationsXml 
        { 
            get 
            { 
                return GetXml(CitationsUrl.ToString());
            } 
        }

        public IEnumerable<Citation> Citations()
        {
            var stream = Restful.ParallelProcessor.InvokePoxWebRequestWithCredentials(CitationsUrl.ToString(), Creds);
            var reader = XmlReader.Create(stream);
            while (reader.NodeType != XmlNodeType.Element) reader.Read();
            XElement article = XElement.Load(reader);

            var results =
                from element in article.Descendants()
                select new Citation(Identifier, element.HasAttributes ? element.FirstAttribute.Value : null, element.Value); 

            return results;
        }
    }
}
