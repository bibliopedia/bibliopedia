using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor;
using Jstor.Domain;

namespace JstorCrawl.Domain
{
    public class SearchAuthor : SearchCommand
    {
        public SearchAuthor(string author)
        {
            Author = author;
        }

        public override string Summary
        {
            get { return "Searching author: " + Author; }
        }

        public virtual string Author { get; protected set; }

        protected override IEnumerable<dc> JstorSearch(Search search)
        {
            return search.SearchByAuthor(Author);
        }
    }
}
