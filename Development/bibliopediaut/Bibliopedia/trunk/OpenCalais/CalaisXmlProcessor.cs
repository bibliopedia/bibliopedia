using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCalais
{
    public class CalaisXmlProcessor : CalaisProcessor
    {
        protected override string ParametersXml()
        {
            return
                string.Format(
                    @"<c:params xmlns:c={0}http://s.opencalais.com/1/pred/{0} xmlns:rdf={0}http://www.w3.org/1999/02/22-rdf-syntax-ns#{0}>
                        <c:processingDirectives c:contentType={0}text/xml{0} c:enableMetadataType={0}GenericRelations{0} c:outputFormat={0}Text/Simple{0} c:docRDFaccesible={0}true{0} >
                        </c:processingDirectives>
                        <c:userDirectives c:allowDistribution={0}true{0} c:allowSearch={0}false{0} c:externalID={0}{0} c:submitter={0}Bibliopedia{0}>
                        </c:userDirectives>
                        <c:externalMetadata>
                        </c:externalMetadata>
                        </c:params>",
                    @"""");
        }
    }
}
