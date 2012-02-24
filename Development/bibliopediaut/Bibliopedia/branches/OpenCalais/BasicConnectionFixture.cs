using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenCalais.Calais;

namespace OpenCalais
{
    [TestFixture]
    public class BasicConnectionFixture
    {
        [Test]
        public void CanExtractSimpleText()
        {
            var response = new CalaisTextProcessor().Process("Barack Obama went to Iraq and bought a Ford Festiva");
            Assert.IsNotEmpty(response);
        }

        [Test]
        public void CanExtractComplexText()
        {
            var file = File.OpenText("sampleinput.txt");
            var response = new CalaisTextProcessor().Process(file.ReadToEnd());
            Assert.IsNotEmpty(response);
        }
    }
}
