using System.Xml;

namespace PublishedWorks.Domain
{
    public class XmlData : MinedData
    {
        public virtual string Xml { get; set; }

        public virtual XmlDocument CreateDocument()
        {
            var doc = new XmlDocument();
            doc.LoadXml(Xml);
            return doc;
        }
    }
}