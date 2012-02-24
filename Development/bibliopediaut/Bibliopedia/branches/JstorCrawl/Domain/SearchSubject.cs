using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor;
using Jstor.Domain;
using Jstor.Domain;

namespace JstorCrawl.Domain
{
    public class SearchSubject : SearchCommand
    {
        public SearchSubject(string subjectName)
            : base(new Search())
        {
            Subject = subjectName;
        }

        public string Subject { get; protected set; }

        public override string Summary
        {
            get { return "Search subject: " + Subject; }
        }

        protected override IEnumerable<dc> JstorSearch(Search search)
        {
            throw new NotImplementedException();
        }
    }
}
