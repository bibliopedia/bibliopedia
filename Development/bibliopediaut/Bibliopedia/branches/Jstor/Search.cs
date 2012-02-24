using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jstor.Domain;
using System.Net;
using Jstor.Domain.Helpers;

namespace Jstor
{
    public class Search
    {
        public static readonly NetworkCredential Creds = new NetworkCredential("mwidner", "biblio$Jstor0");

        public const string SearchString = @"http://dfr.jstor.org/sru/?operation=searchRetrieve&recordPacking=xml&version=1.1&startRecord={0}&maximumRecords={1}&query={2}";
        public static string SearchUrl(string query, int start, int maxToRetrieve)
        {
            var result = String.Format(SearchString, start, maxToRetrieve, query);
            return result;
        }

        searchRetrieveResponse CallJStor(string searchUrl)
        {
            var callbacks = new SearchResponseCallbacks();
            var processor = new Restful.ParallelProcessor(callbacks);
            processor.ProcessWithCredentials(searchUrl, Creds);
            return callbacks.Response;
        }

        public static string EscapeString(string jstorIndex, string term)
        {
            char quot = '"';
            var queryString = Uri.EscapeDataString(jstorIndex+"="+quot+term+quot);
            return queryString;
        }

        public IEnumerable<dc> SearchBySubject(string subject)
        {
            return Execute(EscapeString("dc.subject", subject));
        }

        public IEnumerable<dc> SearchAnywhere(string text)
        {
            return Execute(EscapeString("jstor.text", text));
        }

        protected IEnumerable<dc> Execute(string query)
        {
            var nextRecord = 1;
            int? lastRecord = null;
            do
            {
                var response = CallJStor(SearchUrl(query, nextRecord, 10));
                if (!lastRecord.HasValue)
                {
                    lastRecord = response.numberOfRecords;
                }
                foreach (var item in response.records) yield return item.recordData[0].dc;
                nextRecord = response.nextRecordPosition;
            }
            while (nextRecord < lastRecord);
        }

        public IEnumerable<dc> SearchByAuthor(string author)
        {
            return Execute(EscapeString("dc.creator", author));
        }
    }
}
