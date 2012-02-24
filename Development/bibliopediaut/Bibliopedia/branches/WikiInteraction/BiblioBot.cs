using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetWikiBot;

namespace WikiInteraction
{
    public class BiblioBot : Bot
    {
        public Site Site { get; private set; }

        public BiblioBot()
        {
            Site = new Site("www.bibliopedia.org/smw", "Biblioadmin", "biblio$Wiki0");
        }



    }
}
