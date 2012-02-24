using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JstorCrawl.Domain
{
    public class SearchSubject : SearchCommand
    {
        private string subjectName;

        public SearchSubject(string subjectName)
        {
            // TODO: Complete member initialization
            this.subjectName = subjectName;
        }
        protected override string JstorSearchCode
        {
            get
            {
                {
                    throw new NotImplementedException();
                    return "dc.subject";
                }
            }
        }
    }
}
