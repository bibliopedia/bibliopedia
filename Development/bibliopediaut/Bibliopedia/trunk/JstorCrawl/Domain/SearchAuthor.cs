using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JstorCrawl.Domain
{
    public class SearchAuthor : SearchCommand
    {
        private string authorName;

        public SearchAuthor(string authorName)
        {
            // TODO: Complete member initialization
            this.authorName = authorName;
        }
        protected override string JstorSearchCode
        {
            get
            {
                {
                    throw new NotImplementedException();
                    return "dc.creator";
                }
            }
        }
    }
}
