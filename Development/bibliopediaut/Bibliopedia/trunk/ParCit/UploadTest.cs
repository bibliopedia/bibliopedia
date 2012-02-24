using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ParCit
{
    [TestFixture]
    public class UploadTest
    {
        [Test]
        public void UploadOneFile()
        {
            PushCitations.UploadFile("Temp.txt");
        }
    }
}
