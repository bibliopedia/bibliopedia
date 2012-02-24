using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor.Domain;
using System.Xml;
using System.Xml.Serialization;

namespace Jstor
{
    class SearchResponseCallbacks : Restful.ParallelProcessorCallbacks
    {
        public searchRetrieveResponse Response { get; private set; }

        public override void OnDocument(XmlDocument value)
        {
            // Noop
        }

        public override void OnNode(XmlNode value)
        {
            // +		value	{XmlDeclaration, Value="version=\"1.0\" encoding=\"UTF-8\""}	System.Xml.XmlNode {System.Xml.XmlDeclaration}
            // +		value	{ProcessingInstruction, Name="xml-stylesheet", Value="type=\"text/xsl\" href=\"/site_media/sru/searchRetrieveResponse.xsl\""}	System.Xml.XmlNode {System.Xml.XmlProcessingInstruction}
            // +		value	{Whitespace, Value="\n"}	System.Xml.XmlNode {System.Xml.XmlWhitespace}
            // +		value	{Element, Name="srw:searchRetrieveResponse"}	System.Xml.XmlNode {System.Xml.XmlElement}

            if (value.Name != "srw:searchRetrieveResponse") return;
            var serializer = new XmlSerializer(typeof(JstorService.searchRetrieveResponse));
            var response = serializer.Deserialize(new XmlNodeReader(value)) as JstorService.searchRetrieveResponse;
            Response = new searchRetrieveResponse(response);
        }
    }
}
