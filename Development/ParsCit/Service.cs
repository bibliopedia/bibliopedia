using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bibliopedia.ParsCit
{
    public class Service
    {

        public static string ExtractCitations(string fileName, string groupName)
        {
            var service = new Bibliopedia.ParsCit.ParsCitService();

            string citeFile;
            string bodyFile;
            var result = service.extractCitations("FleckMandeville.txt", "jstor", out citeFile, out bodyFile);
            return result;
        }
    }
}
