using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenCalais;
using Jstor;

namespace CitationSemanticExtraction
{
    [TestFixture]
    public class CombineJStorAndOpenCalais
    {
        [Test]
        public void WhatHappens()
        {
            var citationXml = (new Resource(@"10.2307/1618158")).CitationsXml;
            var calaisResults = new CalaisTextProcessor().Process(citationXml);
            Assert.IsNotEmpty(calaisResults);
        }
    } 
}
