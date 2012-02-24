using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCalais.Calais;

namespace OpenCalais
{
    public abstract class CalaisProcessor
    {
        private const string LicenceId = "ej56hxgrmy44caytx7a3fr6z";
        
        protected abstract string ParametersXml();

        public virtual string Process(string content)
        {
            var service = new calaisSoapClient();

            var body = new EnlightenRequestBody
            {
                content = content,
                paramsXML = ParametersXml(),
                licenseID = LicenceId
            };
            var request = new EnlightenRequest(body);

            var response = service.Enlighten(request);

            return response.Body.EnlightenResult;
        }

    }
}
