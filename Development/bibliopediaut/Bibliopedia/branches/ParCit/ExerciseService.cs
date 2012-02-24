using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net;
using System.ServiceModel;
using System.Threading;

namespace ParCit
{
    [TestFixture]
    public class ExerciseService
    {
        [TestCase("E06-1050.txt", "sample")]
        [TestCase("LomperisMandeville.txt", "sample")]
        public void ConnectLegacy(string fileName, string repository)
        {
            var client = new ParsCit.ParsCitService();
            client.Url = "http://bibliopedia.org:10555";
            string citeFile;
            string bodyFile;
            try
            {
                var result = client.extractCitations(fileName, repository, out citeFile, out bodyFile);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
            }
        }

    }
}
